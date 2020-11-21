var enderecoUrl = "https://localhost:44346";
var produto;
var compra = [];
var totalvenda = 0.0;
var enderecoProduto = "https://localhost:5001/Produto/Produto/";
//var enderecoVenda = "https://localhost:44303/Venda/";
var enderecoVenda = "https://localhost:5001/Venda/";


/*inicio */

atualizarTotal();

/* funções  */

function atualizarTotal() {
    $("#totalvenda").html(totalvenda);
}

function preencherFormulario(dadosProduto) {
    $("#campoNomeProduto").html(dadosProduto.nome);
    $("#campoNomeCategoria").html(dadosProduto.categoria.nome);
    $("#CampoNomeFornecedor").html(dadosProduto.fornecedor.nome);
    $("#CampoPreco").html(dadosProduto.precoDeVenda);
}


function zerarFormulario() {
    $("#campoNomeProduto").html("...");
    $("#campoNomeCategoria").html("...");
    $("#CampoNomeFornecedor").html("...");
    $("#CampoPreco").html("...");
    $("#quantidade").val("");
    $("#codProduto").val("");
}

function adicionarNaTabela(p, qtd) {

    var produtoTemp = {};
    Object.assign(produtoTemp, produto);

    var venda = { produto: produtoTemp, quantidade: qtd, subtotal: produtoTemp.precoDeVenda * qtd };
    //console.log(venda);
    compra.push(venda);

    //mostrar o total da venda
    totalvenda += venda.subtotal;
    atualizarTotal();

    $("#compras").append(` <tr> 
            <td>${p.nome}</td>
            <td>${qtd}</td>
            <td>R$ ${p.precoDeVenda}</td>
            <td>R$ ${p.precoDeVenda * qtd}</td>
            <td>
               <a class='btn btn-danger' data-toggle='tooltip' data-placement='top' title='Deletar' href='#'><i class='fas fa-trash-alt'></i> </a>
            </td>
    </tr>`);
 }


/*ajax*/
$("#pesquisar").click(function () {

    var codigoProduto = $("#codProduto").val();
    var enderecoCompleto = enderecoUrl + "/produto/RetornarProdutoTelaVenda/" + codigoProduto;

    $.post(enderecoCompleto, function (dados, status) {
        produto = dados;
        preencherFormulario(produto);
    }).fail(function () {
        alert("Produto inválido");
    });

});


$("#produtoForm").on("submit", function (event) {
    event.preventDefault();
    var produtoParaTabela = produto;
    var QuantidadeProduto = $("#quantidade").val();
    if (QuantidadeProduto < 1) {
        QuantidadeProduto = 1;
    }
    
    adicionarNaTabela(produtoParaTabela, QuantidadeProduto);

    zerarFormulario();
});


/*finalização de venda*/
$("#finalizarVendaBtn").click(function () {
    if (totalvenda <= 0) {
        alert("Compra inválida, nenhum produto adicionado");
        return;
    }

    var ValorPago = $("#valorpago").val();

    if (!isNaN(ValorPago)) {// not a Number
        ValorPago = parseFloat(ValorPago);

        if (ValorPago >= totalvenda) {
            console.log("calcular troco");
        } else {
            alert("Valor pago e menor que o valor total da compra!");
            return;
        }

    }else{

        alert("Valor pago, inválido!");
        return;
    }

});