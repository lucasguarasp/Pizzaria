var produtos = new Array();
var total = 0;
var ItemProduto = "";

function addCart(id, nome, valor) {
    ItemProduto = "";

    let index = produtos.findIndex(c => c.Id == id);

    if (index < 0) {
        produtos.push({ Id: id, Nome: nome, Valor: valor, Quantidade: 1 });
    } else {
        $(produtos).each(function (i) {
            if (produtos[i].Id == id) {
                produtos[i].Quantidade += 1;
                produtos[i].Valor = valor;
            }
        });
    }

    $("#itens").html(produtos.length);

    ExibirCarrinho();
}

function ExibirCarrinho() {
    $("#itens").html(produtos.length);
    total = 0;
    //exibir Carrinho
    $(produtos).each(function (i) {
        total += produtos[i].Valor * produtos[i].Quantidade;
    });

    $(produtos).each(function (i) {
        ItemProduto += '<li class="list-group-item d-flex justify-content-between lh-condensed">';
        ItemProduto += '<div>';
        ItemProduto += '<small class="my-0">' + produtos[i].Nome + ', R$ ' + produtos[i].Valor + '</small>';
        ItemProduto += '<a class="pull-right">';
        ItemProduto += '<i class="fa fa-trash" onclick="RemoveAll(' + produtos[i].Id + ')">';
        ItemProduto += '</i>';
        ItemProduto += '</a>';
        ItemProduto += '<a class="pull-right">';
        ItemProduto += '<i class="fa fa-remove" onclick="RemoveItem(' + produtos[i].Id + ')">';
        ItemProduto += '</i>&ensp;';
        ItemProduto += '</a>';
        ItemProduto += '</div>';
        ItemProduto += '<div>';
        ItemProduto += '<span data-toggle="tooltip" class="badge bg-green" data-original-title="' + produtos[i].Quantidade + produtos[i].Nome + '">' + produtos[i].Quantidade + 'x</span>';
        ItemProduto += '<span class="text-muted pull-right">R$ ' + (produtos[i].Valor * produtos[i].Quantidade).toFixed(2) + '</span>';
        ItemProduto += '</div>';
        ItemProduto += '</li>';
    });

    $("#produto").html(ItemProduto);
    $("#total").html(total.toFixed(2));
    setCookie();
}

function RemoveAll(id) {

    let index = produtos.findIndex(c => c.Id == id);
    produtos.splice(index, 1);

    ItemProduto = "";
    ExibirCarrinho();
}

function RemoveItem(id) {

    let index = produtos.findIndex(val => val.Id == id);

    if (produtos[index].Quantidade > 1) {
        produtos[index].Quantidade -= 1;
    } else {
        produtos.splice(index, 1);
    }
    ItemProduto = "";
    ExibirCarrinho();
}

function setCookie() {

    document.cookie = "Projeto" + "=" + ('Projeto', JSON.stringify(produtos));
    //var storedAry = JSON.parse(('Projeto'));
    var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    alert(cookieValue);

    let teste = new Array();
    teste.push(cookieValue);

    //teste.splice(cookieValue, 0, cookieValue);
    

};
