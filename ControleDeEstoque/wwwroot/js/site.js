$(document).ready(function () {

    $(".modExcluir").click(function () {

        var id = $(this).data("value");

        $("#conteudoModal").load("/Produtos/ExcluirProduto/" + id,
            function () {
                $("#myModal").modal("show")

            }
        );
    })
});
