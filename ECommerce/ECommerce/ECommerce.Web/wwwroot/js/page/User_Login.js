var User_Login = {
    Init: function () {},
    Login: {
        Login: function () {
            var email = $("#user-login-email").val();
            var password = $("#user-login-password").val();
            var rememberMe = $("#user-login-rememberme").prop("checked");

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
            alert("YAPTIĞIN AYIP");
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
                Helper.UI.Alert("Hata", "Yeni şifreniz uyuşmuyor", "error");
            }
           
            else if (!name || name.length < 2 || name.length> 50) {
                Helper.UI.Alert("Hata", "Adınız 2 karakterden kısa, 50 karakterden uzun olamaz", "error");

            }
            else if (!email || email.length < 6 || email.length > 350 || !Helper.MailCheck(email)) {
                Helper.UI.Alert("Hata", "Emailiniz 6 dan kısa, 350 den uzun olamaz ", "error");
            }
            else if (!surname || surname.length < 2 || surname.length > 50) {
                Helper.UI.Alert("Hata", "Soyadınız 2 karakterden kısa, 50 karakterden uzun olamaz", "error");

            }
            else if (!password || password.length < 8 || password.length > 64) {
                Helper.UI.Alert("Hata", "Şifreniz 8 den kısa, 64 den uzun olamaz", "error");

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


            
            //validation
        },
        Register_Callback: function (result) {
            window.location.reload();
        },
        Register_Callback_Error: function (result) {
            Helper.UI.Alert("Hata Oluştu",result.responseText,"error");
        }
    }
}