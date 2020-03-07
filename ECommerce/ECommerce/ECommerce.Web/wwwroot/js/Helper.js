var Helper = {
    Module: {
        Init: function (name) {
            $.ajax({
                ModuleName: name,
                type: "GET",
                url: "/module/" + name,
                data: [],
                success: Helper.Module.Init_Callbak,
                dataType: "html",
                contentType: "html"
            });
        },
        Init_Callbak: function (result) {
            $("#Module-" + this.ModuleName).html(result);
        }
    }
};