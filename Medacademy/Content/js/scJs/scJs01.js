$(document).on('click', '#login-btn-dummy', function () {
    $('#login-btn').trigger('click');
});

$(document).on('click', '#popup-login-btn-dummy', function () {
    $('#popup-login-btn').trigger('click');
});

$(document).on('click', '#recover-pwd-btn-dummy', function () {
    $('#recover-pwd-btn').trigger('click');
});

$(document).on('click', '#change-pwd-btn-dummy', function () {
    $('#change-pwd-btn').trigger('click');
});

function AdminLoginSuccess(result) {  // 'this' is the form element
    if (result.status) {
        if (result.userType == 1) {
            $('#errorMsg').hide();
            window.location.href = '/Admin/Class';
        }
        else if (result.userType == 2) {
            $('#errorMsg').hide();
            window.location.href = '/Student/Index';
        }
        else if (result.userType == 3) {
            $('#errorMsg').hide();
            window.location.href = '/SuperAdmin/Home';
        }
        else if (result.userType == 4) {
            $('#errorMsg').hide();
            window.location.href = '/SchoolAdmin/Home';
        }
        else if (result.userType == 5) {
            $('#errorMsg').hide();
            window.location.href = '/TeacherAdmin/Home';
        }
    }
    else {
        $.ajax({
            url: '/Account/WrongCredentials/',
            dataType: 'html',
            success: function (data) {
                $('#register-popup').html(data);
                $.validator.unobtrusive.parse($('#register-popup'));
                $('#register-popup').modal('show');
                $('body').pleaseWait('stop');
            },
        });
    }
}

function AdminLoginFailed(result) {
    console.log(result.msg);
}

//Loader
$(document).ajaxStart(function () {
    $(":submit").attr("disabled", true);
    //$('body').pleaseWait();
    ajaxLoaderStart();
});

$(document).ajaxComplete(function () {
    $(":submit").attr("disabled", false);
    //$('body').pleaseWait('stop');
    ajaxLoaderStop();
});

function showLoader() {
    $('body').pleaseWait();
    setTimeout(function () {
        $('body').pleaseWait('stop');
    }, 1250)
}

function permLoader() {
    $('body').pleaseWait();
}

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 32 && charCode != 43 && charCode != 44 && charCode != 45 && charCode != 46 && charCode > 31
      && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

function validateCharacters(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 32 && charCode != 45 && charCode != 44 && (charCode < 65 || charCode > 90 && charCode < 97 || charCode > 122))
        return false;
    return true;
}

function validatePostalCode(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode < 48 || charCode > 57)
        return false;
    return true;
}

function validateSchoolName(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode;
    if (charCode != 32 && charCode != 45 && charCode != 44 && charCode != 44 && charCode != 46 && charCode != 39 &&
        (charCode < 65 || charCode > 90 && charCode < 97 || charCode > 122))
        return false;
    return true;
}

//$(document).on('blur', '#email-validation-jq', function () {
//    var Email = $('#email-validation-jq').val();
//    if (Email.length > 0) {
//        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
//        if (regex.test(Email)) {
//            $.ajax({

//                method: "GET",
//                url: "/Account/IsEmailExist?Email=" + Email,
//                type: 'json'
//            })
//                .done(function (result) {
//                    if (result.status) {
//                        $('#EmailAvail').hide();
//                        $('#EmailExist').show();
//                        $('#EmailNotValid').hide();
//                        //$('#BtnSubmit').attr("disabled", true);
//                        disableRegisterButton();
//                        //$('#submit-btn-dummy-reg').show();
//                        //$('#submit-btn-dummy-reg-fake').show();
//                    }
//                    else {
//                        $('#EmailExist').hide();
//                        $('#EmailAvail').show();
//                        $('#EmailNotValid').hide();
//                        //$('#BtnSubmit').removeAttr("disabled");
//                        enableRegisterButton();
//                        //$('#submit-btn-dummy-reg').show();
//                        //$('#submit-btn-dummy-reg-fake').hide();
//                    }
//                });
//        }
//        else {
//            $('#EmailNotValid').show();
//            $('#EmailAvail').hide();
//            $('#EmailExist').hide();
//            //$('#BtnSubmit').attr("disabled", true);
//            disableRegisterButton();
//            //$('#submit-btn-dummy-reg').show();
//            //$('#submit-btn-dummy-reg-fake').show();
//        }
//    }
//    else {
//        $('#EmailNotValid').show();
//        $('#EmailNotValid').text("Email Required");
//    }
//});

//Not using
//$(document).on('blur', '.email-clear', function () {
//    var Email = $('.email-clear').val();
//    if (Email.length > 0) {
//        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
//        if (regex.test(Email)) {
//            $('#EmailNotValid').hide();
//            $('#submit-btn-dummy-reg').removeAttr("disabled");
//        }
//        else {
//            $('#EmailNotValid').show();
//            $('#submit-btn-dummy-reg').prop("disabled", true);
//        }
//    }
//    else
//        $('#EmailNotValid').hide();
//});
////Not using
//$(document).on('keyup', '#email-validation-refer', function () {
//    var Email = $('#email-validation-refer').val();
//    if (Email.length > 0) {
//        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
//        if (regex.test(Email)) {
//        }
//        else {
//            $('#EmailNotValid').show();
//        }
//    }
//});

$(document).on('blur', '.firstNameValid', function () {
    validateFirstName();
});

function validateFirstName() {
    var status = 1;
    if ($('.firstNameValid').val().length > 0) {
        //if ($('.firstNameValid').val().length == 60) {
        //    $('.firstNameMsg').text("");
        //}
        $('.firstNameMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.firstNameMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('blur', '.lastNameValid', function () {
    validateLastName();
});

function validateLastName() {
    var status = 1;
    if ($('.lastNameValid').val().length > 0) {
        $('.lastNameMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.lastNameMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('keyup', '.passwordValid', function () {
    validatePassword();
});

$(document).on('blur', '.passwordValid', function () {
    validatePasswordOnBlur();
});

function validatePasswordOnBlur() {
    var status = 1;
    if ($('.passwordValid').val().length > 0) {
        $('.passwordMsg').hide();
        if ($('.passwordValid').val().length < 6) {
            $('.passwordMsg').text("Minimum 6 characters required!");
            $('.passwordMsg').show();
            disableRegisterButton();
            status = 0;
        }
        else {
            $('.passwordMsg').hide();
            enableRegisterButton();
        }
    }
    else {
        $('.passwordMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

function validatePassword() {
    var status = 1;
    if ($('.passwordValid').val().length < 6) {
        $('.passwordMsg').text("Minimum 6 characters required!");
        $('.passwordMsg').show();
        disableRegisterButton();
        status = 0;
    }
    else {
        $('.passwordMsg').hide();
        enableRegisterButton();
    }


    return status;
}

//$(document).on('blur', '.confirmPwdValid', function () {
//    validateConfirmPassword();
//});

//function validateConfirmPassword() {
//    var status = 1;
//    var pwd = $('.passwordValid').val();
//    var cpwd = $('.confirmPwdValid').val();
//    if ($('.passwordValid').val().length > 0) {
//        if ($('.confirmPwdValid').val().length > 0) {
//            if (pwd != cpwd) {
//                $('.confirmPwdMsg').show();
//                $('.confirmPwdMsg').text("Password not matching!");
//                disableRegisterButton();
//                status = 0;
//            }
//            else {
//                $('.confirmPwdMsg').hide();
//                enableRegisterButton();
//            }
//        }
//        else {
//            $('.confirmPwdMsg').text("Confirm password required!");
//            $('.confirmPwdMsg').show();
//            disableRegisterButton();
//            status = 0;
//        }
//    }
//    else {
//        $('.confirmPwdMsg').hide();
//        disableRegisterButton();
//        status = 0;
//    }
//    return status;
//}

$(document).on('blur', '#email-validation-jq', function () {
    validateEmail();
});

function validateEmail() {
    var status = 1;
    var Email = $('#email-validation-jq').val();
    if (Email.length > 0) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test(Email)) {
            $.ajax({

                method: "GET",
                url: "/Account/IsEmailExist?Email=" + Email,
                type: 'json'
            })
                .done(function (result) {
                    if (result.status) {
                        $('#EmailAvail').hide();
                        $('#EmailExist').show();
                        $('#EmailNotValid').hide();
                        //$('#BtnSubmit').attr("disabled", true);
                        //$('#submit-btn-dummy-reg').show();
                        disableRegisterButton();
                        $('html,body').animate({
                            scrollTop: $('#email-validation-jq').offset().top
                        }, 'slow');
                        disableRegisterButton();
                        status = 0;
                        $('#hfEmailValid').val(0);
                        //$('#submit-btn-dummy-reg-fake').show();
                    }
                    else {
                        $('#EmailExist').hide();
                        $('#EmailAvail').show();
                        $('#EmailNotValid').hide();
                        enableRegisterButton();
                        $('#hfEmailValid').val(1);
                        //$('#BtnSubmit').removeAttr("disabled");
                        //$('#submit-btn-dummy-reg').show();
                        //$('#submit-btn-dummy-reg-fake').hide();
                    }
                });
        } 
        else {
            $('#EmailNotValid').show();
            $('#EmailNotValid').text("Email required");
            $('#EmailAvail').hide();
            $('#EmailExist').hide();
            disableRegisterButton();
            $('#hfEmailValid').val(0);
            //$('#BtnSubmit').attr("disabled", true);
            //$('#submit-btn-dummy-reg').show();
            $('html,body').animate({
                scrollTop: $('#email-validation-jq').offset().top
            }, 'slow');
            status = 0;
           
            //$('#submit-btn-dummy-reg-fake').show();
        }
    }
    else {
        $('#EmailNotValid').show();
        $('#EmailNotValid').text("Email required");
        disableRegisterButton();
        status = 0;
        $('#hfEmailValid').val(0);
    }
   
    return status;
}



$(document).ready(function () {

    //$('.dobValid').focusin(function () {
        
    //    $('.dobMsg').hide();

    //    $('.ic__day').click(function () {
    //        validateDob();

    //    });

    //});

    //$('.dobValid').focusout(function () {

    //    validateDob();
    //});

    //$('.dobValid').change(function () {

    //    validateDob();
    //});
    $('.ExpertNameNotValid').hide();////

    

});

//function validateDob() {
//    var status = 1;
//    if ($('.dobValid').val().length > 0) {
//        $('.dobMsg').hide();
//        enableRegisterButton();
//    }
//    else {
//        $('.dobMsg').show();
//        disableRegisterButton();
//        status = 0;
//    }
//    return status;
//}



$(document).on('blur', '.contactValid', function () {
    validateContact();
});

$(document).on('keyup', '.contactValid', function () {
    validateContactKeyUp();
});

function validateContactKeyUp() {
    var status = 1;

    if ($('.contactValid').val().length < 5) {
        $('.contactMsg').hide();
        $('.contactMsg').text("Minimum 5 numbers required");
        $('.contactMsg').show();
        disableRegisterButton();
        status = 0;
    }
    else {
        $('.contactMsg').hide();
        enableRegisterButton();
    }
    return status;
}

function validateContact() {
    var status = 1;
    if ($('.contactValid').val().length > 0) {
        $('.contactMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.contactMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('blur', '.addressValid', function () {
    validateAddress();
});

function validateAddress() {
    var status = 1;
    if ($('.addressValid').val().length > 0) {
        $('.addressMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.addressMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('blur', '.postalValid', function () {
    validatePostal();
});

function validatePostal() {
    var status = 1;
    if ($('.postalValid').val().length > 0) {
        $('.postalMsg').hide();
        if ($('.postalValid').val().length < 6) {
            $('.postalMsg').text("Minimum 6 numbers required");
            $('.postalMsg').show();
            disableRegisterButton();
            status = 0;
        }
        else {
            $('.postalMsg').hide();
            enableRegisterButton();
        }
    }
    else {
        $('.postalMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('keyup', '.postalValid', function () {
    validatePostalKeyUp();
});

function validatePostalKeyUp() {
    var status = 1;

    $('.postalMsg').hide();
    if ($('.postalValid').val().length < 6) {
        $('.postalMsg').text("Minimum 6 numbers required");
        $('.postalMsg').show();
        disableRegisterButton();
        status = 0;
    }
    else {
        $('.postalMsg').hide();
        enableRegisterButton();
    }


    return status;
}

$(document).on('blur', '.schoolValid', function () {
    validateSchool();
});

function validateSchool() {
    var status = 1;
    if ($('.schoolValid').val().length > 0) {
        $('.schoolMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.schoolMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('blur', '.locationValid', function () {
    validateLocation();
});

function validateLocation() {
    var status = 1;
    if ($('.locationValid').val().length > 0) {
        $('.locationMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.locationMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('blur', '.stateValid', function () {
    validateState();
});

function validateState() {
    var status = 1;
    if ($('.stateValid').val().length > 0) {
        $('.stateMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.stateMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('change', '.classValid', function () {
    validateClass();
});

function validateClass() {
    var status = 1;
    if ($('.classValid').val() != "") {
        $('.classMsg').hide();
        enableRegisterButton();
    }
    else {
        $('.classMsg').show();
        disableRegisterButton();
        status = 0;
    }
    return status;
}

function enableRegisterButton() {
    //$('#BtnSubmit').removeAttr("disabled");
    $('#BtnSubmit').removeAttr("disabled");
    //$('#submit-btn-dummy-reg').show();
    //$('#submit-btn-dummy-reg-fake').hide();
}

function disableRegisterButton() {
    //$('#BtnSubmit').attr("disabled", true);
    $('#BtnSubmit').attr("disabled", true);
    //$('#submit-btn-dummy-reg').hide();
    //$('#submit-btn-dummy-reg-fake').show();
}

function checkAllFields() {

    var status = 0;
    //FirstName
    status = validateFirstName();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.firstNameValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //LastName
    status = validateLastName();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.lastNameValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //Email
    status = validateEmail();
    if (status == 0) {
        
        $('html,body').animate({
            scrollTop: $('#email-validation-jq').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
 
    //Password
    status = validatePassword();
    if (status == 0) {
        $('html, body').animate({
            scrollTop: $('.passwordValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //Confirm Password
    //status = validateConfirmPassword();
    //if (status == 0) {
    //    $('html, body').animate({
    //        scrollTop: $('.confirmPwdValid').offset().top
    //    },
    //    'slow');
    //    $('body').pleaseWait('stop');
    //    return;
    //}

    //DOB
    //status = validateDob();
    //if (status == 0) {
    //    $('html, body').animate({
    //        scrollTop: $('.dobValid').offset().top
    //    },
    //    'slow');
    //    $('body').pleaseWait('stop');
    //    return;
    //}
    //Contact
    status = validateContact();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.contactValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    status = validateContactKeyUp();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.contactValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //Address
    status = validateAddress();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.addressValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //Postal
    status = validatePostal();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.postalValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    status = validatePostalKeyUp();
    if (status == 0)
    {
        $('html,body').animate({
            scrollTop: $('.postalValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //School
    status = validateSchool();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.schoolValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //Location
    status = validateLocation();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.locationValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //State
    status = validateState();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.stateValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    //Class
    status = validateClass();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.classValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    var validEmail = $('#hfEmailValid').val();
    if (status == 1 && validEmail == 1) {
        $('body').pleaseWait();
        $('#BtnSubmit').trigger('click');
        //Updation
        //$('.show-hide-register-dev').addClass('isHidden');
        //$("html, body").animate({
        //    scrollTop: $('.show-hide-payment-dev').offset().top
        //}, 100);
        //$('.show-hide-payment-dev').removeClass('isHidden');
        //$('.addressPayCashValid').val($('.addressValid').val());
        //$('.statePayCashValid').val($('.stateValid').val());
        //$('.postalPayCashValid').val($('.postalValid').val());
        //$('.contactPayCashValid').val($('.contactValid').val());
        //$('body').pleaseWait('stop');
    }
}

$(window).load(function () {
    //$('#datepicker-1').click(function () {
    //    setTimeout(function () {
    //        $('.ui-state-default').click(function () {
    //            $('.dobMsg').hide();
    //        });
    //    }, 300);
    //});
});

$(document).on('click', '.dev-contact-expert-student', function () {
    $('.submit-contact-expert-student').trigger('click');
});


$(window).load(function () {

    loaderStop();
});

$(document).ready(function () {

    loaderStart();
});

function loaderStart() {
    $('.aps-i-want-loader').show('slow');
    $('.aps-i-want-loader').each(function () {
        var t = $(this);
        t.append("<div class='aps-loader-landing'><div class='aps-loader-content'><div class='aps-loader-holder'><div class='aps-loader-content-holder'> <div class='aps-loader-outer-round'><img src='/Content/aps-loader-image/round.png' /></div><div class='aps-loader-inner-face'><img src='/Content/aps-loader-image/face.png' /></div><div class='aps-loader-cap'><img src='/Content/aps-loader-image/cap.png' /></div></div></div></div></div>");
        var loader_height = t.children('.aps-loader-landing').children('.aps-loader-content').height();

        t.css('height', $(window).height());
        t.css('width', $(window).width());
        t.css('position', 'fixed');
        t.css('top', '0px');
        t.css('left', '0px');
        //alert(loader_height);
        var my_height = t.height();
        if (my_height > $(window).height()) {
            my_height = $(window).height();
        }
        var halfofloader_height = loader_height / 2;

        if (my_height > loader_height) {
            var extra_gap = (my_height - loader_height) / 2 - halfofloader_height;
            t.children('.aps-loader-landing').css('padding-top', extra_gap);
        }
    });
}


function loaderStop() {
    $('.aps-i-want-loader-page').show();
    var pre_hight = $('.nav_bar_main').height();
    $('body').css('padding-top', pre_hight);
    setTimeout(function () {
        $('.aps-i-want-loader').html("");
        $('.aps-i-want-loader').hide();
    }, 500);
    $('.aps-i-want-loader-page').show();
}

function ajaxLoaderStart() {
    // $('.aps-i-want-loader').show('slow');
    $('.aps-i-want-loader').each(function () {
        var t = $(this);
        t.append("<div class='aps-loader-landing' style = 'background-color:rgba(255, 255, 255, 0.5)'><div class='aps-loader-content'><div class='aps-loader-holder'><div class='aps-loader-content-holder'> <div class='aps-loader-outer-round'><img src='/Content/aps-loader-image/round.png' /></div><div class='aps-loader-inner-face'><img src='/Content/aps-loader-image/face.png' /></div><div class='aps-loader-cap'><img src='/Content/aps-loader-image/cap.png' /></div></div></div></div></div>");
        var loader_height = t.children('.aps-loader-landing').children('.aps-loader-content').height();

        t.css('height', $(window).height());
        t.css('width', $(window).width());
        t.css('position', 'fixed');
        t.css('top', '0px');
        t.css('left', '0px');
        //alert(loader_height);
        var my_height = t.height();
        if (my_height > $(window).height()) {
            my_height = $(window).height();
        }
        var halfofloader_height = loader_height / 2;

        if (my_height > loader_height) {
            var extra_gap = (my_height - loader_height) / 2 - halfofloader_height;
            t.children('.aps-loader-landing').css('padding-top', extra_gap);
        }

        t.css('display','block');
    });
}

function ajaxLoaderStop() {
    $('.aps-i-want-loader-page').show();
    var pre_hight = $('.nav_bar_main').height();
    $('body').css('padding-top', pre_hight);
    setTimeout(function () {
        $('.aps-i-want-loader').html("");
        $('.aps-i-want-loader').hide();
    }, 0);
}


//PayByCashValidation

//Address
$(document).on('blur', '.addressPayCashValid', function () {
    validatePayCashAddress();
});

function validatePayCashAddress() {
    var status = 1;
    if ($('.addressPayCashValid').val().length > 0) {
        $('.addressPayCashMsg').hide();
        enablePayCashRegisterButton();
    }
    else {
        $('.addressPayCashMsg').show();
        disablePayCashRegisterButton();
        status = 0;
    }
    return status;
}
//Address

//State
$(document).on('blur', '.statePayCashValid', function () {
    validatePayCashState();
});

function validatePayCashState() {
    var status = 1;
    if ($('.statePayCashValid').val().length > 0) {
        $('.statePayCashMsg').hide();
        enablePayCashRegisterButton();
    }
    else {
        $('.statePayCashMsg').show();
        disablePayCashRegisterButton();
        status = 0;
    }
    return status;
}
//State

//Postal
$(document).on('blur', '.postalPayCashValid', function () {
    validatePayCashPostal();
});

function validatePayCashPostal() {
    var status = 1;
    if ($('.postalPayCashValid').val().length > 0) {
        $('.postalPayCashMsg').hide();
        if ($('.postalPayCashValid').val().length < 6) {
            $('.postalPayCashMsg').text("Minimum 6 numbers required");
            $('.postalPayCashMsg').show();
            disablePayCashRegisterButton();
            status = 0;
        }
        else {
            $('.postalPayCashMsg').hide();
            enablePayCashRegisterButton();
        }
    }
    else {
        $('.postalPayCashMsg').show();
        disablePayCashRegisterButton();
        status = 0;
    }
    return status;
}

$(document).on('keyup', '.postalPayCashValid', function () {
    validatePayCashPostalKeyUp();
});

function validatePayCashPostalKeyUp() {
    var status = 1;

    $('.postalPayCashMsg').hide();
    if ($('.postalPayCashValid').val().length < 6) {
        $('.postalPayCashMsg').text("Minimum 6 numbers required");
        $('.postalPayCashMsg').show();
        disablePayCashRegisterButton();
        status = 0;
    }
    else {
        $('.postalPayCashMsg').hide();
        enablePayCashRegisterButton();
    }
    return status;
}
//Postal

//Contact
$(document).on('blur', '.contactPayCashValid', function () {
    validatePayCashContact();
});

$(document).on('keyup', '.contactPayCashValid', function () {
    validatePayCashContactKeyUp();
});

function validatePayCashContactKeyUp() {
    var status = 1;

    if ($('.contactPayCashValid').val().length < 5) {
        $('.contactPayCashMsg').hide();
        $('.contactPayCashMsg').text("Minimum 5 numbers required");
        $('.contactPayCashMsg').show();
        disablePayCashRegisterButton();
        status = 0;
    }
    else {
        $('.contactPayCashMsg').hide();
        enablePayCashRegisterButton();
    }
    return status;
}

function validatePayCashContact() {
    var status = 1;
    if ($('.contactPayCashValid').val().length > 0) {
        $('.contactPayCashMsg').hide();
        enablePayCashRegisterButton();
    }
    else {
        $('.contactPayCashMsg').show();
        disablePayCashRegisterButton();
        status = 0;
    }
    return status;
}
//Contact

function checkAllFieldsInPayCash() {

    var status = 0;
    //Contact
    status = validatePayCashContact();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.contactPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    status = validatePayCashContactKeyUp();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.contactPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }

    //Address
    status = validatePayCashAddress();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.addressPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }

    //Postal Code
    status = validatePayCashPostal();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.postalPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    status = validatePayCashPostalKeyUp();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.postalPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    
    //State
    status = validatePayCashState();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.statePayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }

    if (status == 1) {
        $('body').pleaseWait();
        $('#BtnPayCashSubmit').trigger('click');
    }
}
function checkAllFieldsInPayByCash() {

    var status = 0;
    //Contact
    status = validatePayCashContact();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.contactPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    status = validatePayCashContactKeyUp();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.contactPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }

    //Address
    status = validatePayCashAddress();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.addressPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }

    //Postal Code
    status = validatePayCashPostal();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.postalPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }
    status = validatePayCashPostalKeyUp();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.postalPayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }

    //State
    status = validatePayCashState();
    if (status == 0) {
        $('html,body').animate({
            scrollTop: $('.statePayCashValid').offset().top
        },
        'slow');
        $('body').pleaseWait('stop');
        return;
    }

    if (status == 1) {
        $('body').pleaseWait();
        $('#BtnPayCash').trigger('click');
    }
}

function enablePayCashRegisterButton() {
    $('#BtnPayCashSubmit').removeAttr("disabled");
}

function disablePayCashRegisterButton() {
    $('#BtnPayCashSubmit').attr("disabled", true);
}


//Validation Refer Friend

//Name
$(document).on('blur', '.nameReferFriendValid', function () {
    validateReferFriendName();
});

function validateReferFriendName() {
    var status = 1;
    if ($('.nameReferFriendValid').val().length > 0) {
        $('.nameReferFriendMsg').hide();
        enableReferFriendRegisterButton();
    }
    else {
        $('.nameReferFriendMsg').show();
        disableReferFriendRegisterButton();
        status = 0;
    }
    return status;
}
//Name

//Contact
$(document).on('blur', '.contactReferFriendValid', function () {
    validateReferFriendContact();
});

$(document).on('keyup', '.contactPayCashValid', function () {
    validateReferFriendContactKeyUp();
});

function validateReferFriendContactKeyUp() {
    var status = 1;

    if ($('.contactReferFriendValid').val().length < 5) {
        $('.contactReferFriendMsg').hide();
        $('.contactReferFriendMsg').text("Minimum 5 numbers required");
        $('.contactReferFriendMsg').show();
        disableReferFriendRegisterButton();
        status = 0;
    }
    else {
        $('.contactReferFriendMsg').hide();
        enableReferFriendRegisterButton();
    }
    return status;
}

//function validateReferFriendContact() {
//    var status = 1;
//    if ($('.contactReferFriendValid').val().length > 0) {
//        $('.contactReferFriendMsg').hide();
//        enableReferFriendRegisterButton();
//    }
//    else {
//        $('.contactReferFriendMsg').show();
//        disableReferFriendRegisterButton();
//        status = 0;
//    }
//    return status;
//}
//Contact

//Email
$(document).on('blur', '#email-validation-refer', function () {
    validateReferFriendEmail();
});

function validateReferFriendEmail() {
    var status = 1;
    var Email = $('#email-validation-refer').val();
    if (Email.length > 0) {

        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test(Email)) {
    
             
                   
            
            $('#EmailNotValid').hide();
            enableReferFriendRegisterButton();
            //  $('#hfEmailValid').val(1);
            //$('#BtnSubmit').removeAttr("disabled");
            //$('#submit-btn-dummy-reg').show();
            //$('#submit-btn-dummy-reg-fake').hide();
        
        }
        else {
            $('#EmailNotValid').show();
            // $('#EmailNotValid').text("Email required");
         
            disableReferFriendRegisterButton();
            // $('#hfEmailValid').val(0);
            //$('#BtnSubmit').attr("disabled", true);
            //$('#submit-btn-dummy-reg').show();
            $('html,body').animate({
                scrollTop: $('#email-validation-refer').offset().top
            }, 'slow');
            status = 0;

            //$('#submit-btn-dummy-reg-fake').show();
        }
    }
    else {
        $('#EmailNotValid').show();
        $('#EmailNotValid').text("Email required");
        disableReferFriendRegisterButton();
        status = 0;
        //  $('#hfEmailValid').val(0);
    }

    return status;
}

// Email

function enableReferFriendRegisterButton() {
    $('#btn-submit-fake').removeAttr("disabled");
}

function disableReferFriendRegisterButton() {
    $('#btn-submit-fake').attr("disabled", true);
}



// Paul ------ email validation for Schooladmin
$(document).on('blur', '.email-validation-admin', function () {
  
    validateEmailAdmin();
});

function validateEmailAdmin() {
    var Email = $('.email-validation-admin').val();
    if (Email.length > 0) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test(Email)) {
            $.ajax({

                method: "GET",
                url: "/SchoolAdmin/IsEmailExist?Email=" + Email,
                type: 'json'
            })
                .done(function (result) {

                    if (result.status) {
                        $('#EmailAvail').hide();
                        $('#EmailExist').show();
                        disableRegisterButton();

                    }
                    else {
                        $('#EmailExist').hide();
                        $('#EmailAvail').show();
                        $('#EmailNotValid').hide();
                        enableRegisterButton();
                    }
                });
        }
    }
    return status;
}

function disableRegisterButton()
{
    $('#submit').addClass('isDisabled');
}
function enableRegisterButton() {

    $('#submit').removeClass('isDisabled');
}
//--------------------------------------------------------
    
// Paul ------ email validation for Super admin-----------
$(document).on('blur', '.email-validation-super-admin', function () {
  
    validateEmailSuperAdmin();
});

function validateEmailSuperAdmin() {
    var Email = $('.email-validation-super-admin').val();
    if (Email.length > 0) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (regex.test(Email)) {
            $.ajax({

                method: "GET",
                url: "/SuperAdmin/IsEmailExist?Email=" + Email,
                type: 'json'
            })
                .done(function (result) {

                    if (result.status) {
                        $('#EmailAvail').hide();
                        $('#EmailExist').show();
                        disableRegisterButton();

                    }
                    else {
                        $('#EmailExist').hide();
                        $('#EmailAvail').show();
                        $('#EmailNotValid').hide();
                        enableRegisterButton();
                    }
                });
        }
    }
    return status;
}

function disableRegisterButton()
{
    $('#submit').addClass('isDisabled');
}
function enableRegisterButton() {

    $('#submit').removeClass('isDisabled');
}
