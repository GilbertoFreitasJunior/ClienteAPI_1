$(function () {
    var resultado = $("#resultado");
    var cliente = $("#cliente");
    var mensagem = $("#message");
    var loading = $(".processandoMensagem");

    loading.hide();
    resultado.hide();

    $("#pesquisarBtn").on("click", function (e) {
        e.preventDefault();
        cliente.text("");
        mensagem.text("");

        var dddVal = $("#ddd").val();
        var foneVal = $("#fone").val();
        var cnpjVal = $("#cp").val();

        if (dddVal.length > 0 && foneVal.length > 0) {
            loading.show();
            $.ajax({
                type: "post",
                url: "/Home/GetClienteFone",
                data: { ddd: dddVal, fone: foneVal },
                success: function (obj) {
                    AjaxSuccess(obj);
                },
                error: function (response) {
                    if (response.status == 500) NenhumCliente();
                }
            })
        }
        else if (cnpjVal.length > 0) {
            loading.show();
            $.ajax({
                type: "post",
                url: "/Home/GetClienteCnpj",
                data: { cnpj: cnpjVal },
                success: function (obj) {
                    AjaxSuccess(obj);
                },
                error: function (response) {
                    if (response.status == 500) NenhumCliente();
                }
            })
        } else {
            mensagem.text("Insira algum dado de pesquisa!");
        }
    });

    function AjaxSuccess(obj) {
        loading.hide();
        if (obj == null) {
            NenhumCliente();
            return;
        }

        CriaCliente(obj);
    }

    function CriaCliente(obj) {
        resultado.show();
        cliente.text("");
        mensagem.text("Resultado:");

        if (typeof obj == "array")
            var txt = obj[0];
        else
            var txt = obj;

        var tr = $(document.createElement("tr"));
        var th = $(document.createElement("th"));
        th.text(txt.id);
        th.attr("scope", "row");
        tr.append(th);

        var th = $(document.createElement("th"));
        th.text(txt.nome);
        tr.append(th);

        var th = $(document.createElement("th"));
        th.text(txt.ddd);
        tr.append(th);

        var th = $(document.createElement("th"));
        th.text(txt.fone);
        tr.append(th);

        var th = $(document.createElement("th"));
        th.text(txt.cnpJouCpf);
        tr.append(th);

        cliente.append(tr);
    }

    function NenhumCliente() {
        loading.hide();
        mensagem.text("Nenhum cliente encontrado!");
        resultado.hide();
        cliente.text("");
    }
});
