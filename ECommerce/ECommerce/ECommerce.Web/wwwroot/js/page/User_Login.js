﻿var User_Login = {
    Init: function () { },
    Login: {
        Login: function () {
            var email = $("#user-login-email").val();
            var password = $("#user-login-password").val();
            var rememberMe = $("#user-login-rememberme").prop("checked");

            if (!email || !Helper.MailCheck(email)) {
                Helper.UI.Alert("HATA", "E-posta adresiniz hatalı.", "error");
                return;
            }
            else if (!password ||password.length <8 || password.length > 64) {
                Helper.UI.Alert("HATA", "Şifreniz hatlı.", "error");
                return;
            }

            var data = { Email: email, Password: password, RememberMe: rememberMe };

            data = JSON.stringify(data);

            $.ajax({
                type: "POST",
                url: "/user/loginaction",
                data: data,
                success: User_Login.Login.Login_Callback,
                error: User_Login.Login.Login_Callback_Error,
                dataType: "json",
                contentType: "application/json; charset=utf-8;"
            });
        },
        Login_Callback: function (result) {
            window.location = "/";
        },
        Login_Callback_Error: function (result) {
            Helper.UI.Alert("HATA", result.responseText, "error");
        }
    },
    Register: {
        Register: function () {
            var name = $("#user-register-name").val();
            var surname = $("#user-register-surname").val();
            var email = $("#user-register-email").val();
            var password = $("#user-register-password").val();
            var password2 = $("#user-register-password2").val();

            if (password != password2) {
                Helper.UI.Alert("Hata!", "Şifreler birbiriyle uyuşmuyor!", "error")
                return;
            }
            else if (!name ||name.length < 2 || name.length > 50) {
                Helper.UI.Alert("Hata!", "Lütfen geçerli bir isim giriniz!", "error")
                return;
            }
            else if (!surname || surname.length < 2 || surname.length > 50) {
                Helper.UI.Alert("Hata!", "Lütfen geçerli bir soyisim giriniz!", "error")
                return;
            }
            else if (!email || email.length < 6 || email.length > 350 || !Helper.MailCheck(email)) {
                Helper.UI.Alert("Hata!", "Lütfen geçerli bir email giriniz.", "error")
                return;
            } 
            else if (!password || password.length < 8 || password.length > 64) {
                Helper.UI.Alert("Hata!", "Lütfen geçerli bir şifre giriniz!", "error")
                return;
            }
            else {
                var data = { Name: name, Surname: surname, Email: email, Password: password };

                data = JSON.stringify(data);

                $.ajax({
                    type: "POST",
                    url: "/user/registeraction",
                    data: data,
                    success: User_Login.Register.Register_Callback,
                    error: User_Login.Register.Register_Callback_Error,
                    dataType: "json",
                    contentType: "application/json; charset=utf-8;"
                });
            }
            //client side validation
            //send to server
        },
        Register_Callback: function () {
            window.location.reload();
        },
        Register_Callback_Error: function (result) {
            Helper.UI.Alert("Hata Oluştu", result.responseText, "error");
        }
    }
};