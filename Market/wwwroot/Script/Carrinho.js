var produtos = new Array();
var total = 0;
var ItemProduto = "";

//toda vez que é add um produto essa fuction é chamada
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

//adiciona front do carrinho
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

//remove apenas todas quantidades do produto
function RemoveAll(id) {

    let index = produtos.findIndex(c => c.Id == id);
    produtos.splice(index, 1);

    ItemProduto = "";

    if (window.location.pathname === "/Checkout/Checkout") {
        setCookie();
        window.location = "/Checkout/Checkout";
    } else {
        ExibirCarrinho();
    }

}

//remove apenas uma quantidade do produto
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

//cria cookie a partir do carrinho
function setCookie() {
    document.cookie = "Projeto" + "=" + (JSON.stringify(produtos));
    var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    //var arrayProdutos = $.parseJSON(cookieValue); 

};

//chama controoler e envia array de produtos
function Checkout() {
    var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var arrayProdutos = $.parseJSON(cookieValue);

    $.ajax({
        type: "GET",
        url: '/Checkout/Checkout',
        data: {
            'produtos': arrayProdutos
        },
        success: function (data) {
            alert('Enviado');
        },
        error: function (error) {
            alert('Nao enviou');
        }
    });


}

$(document).ready(function () {
    if (window.location.pathname === "/Checkout/Checkout") {
        var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");

        if (cookieValue != "") {
            produtos = $.parseJSON(cookieValue);
        }

        total = 0;
        $(produtos).each(function (i) {
            total += produtos[i].Valor * produtos[i].Quantidade;
        });

        ItemProduto = "";
        $(produtos).each(function (i) {
            ItemProduto += '<tr>';
            ItemProduto += '<td data-th="Product">';
            ItemProduto += '<div class="row">';
            ItemProduto += '<div class="col-sm-2 hidden-xs"><img src="http://placehold.it/100x100" alt="..." class="img-responsive"/></div>';
            ItemProduto += '<div class="col-sm-10">';
            ItemProduto += '<h4 class="nomargin">' + produtos[i].Nome + '</h4>';
            ItemProduto += '<p>Quis aute iure reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Lorem ipsum dolor sit amet.</p>';
            ItemProduto += '</div>';
            ItemProduto += '</div>';
            ItemProduto += '</td>';
            ItemProduto += '<td data-th="Price">R$ ' + produtos[i].Valor + '</td>';
            ItemProduto += '<td data-th="Quantity">';
            ItemProduto += '<input type="number" class="form-control text-center" value="' + produtos[i].Quantidade + '">';
            ItemProduto += '</td>';
            ItemProduto += '<td data-th="Subtotal" class="text-center">' + (produtos[i].Valor * produtos[i].Quantidade).toFixed(2) + '</td>';
            ItemProduto += '<td class="actions pull-right" data-th="">';
            ItemProduto += '<button class="btn btn-danger btn-sm" onclick="RemoveAll(' + produtos[i].Id + ')"><i class="fa fa-trash-o"></i></button>';
            ItemProduto += '</td>';
            ItemProduto += '</tr>';
        });

        $("#produto").html(ItemProduto);
        $("#total").html("R$ " + total.toFixed(2));

    }
})

//adiciona itens no carrinho se existir cookie
$(document).ready(function () {
    if (window.location.pathname === "/Checkout/Index") {
        var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");

        if (cookieValue != "") {
            produtos = $.parseJSON(cookieValue);
            ExibirCarrinho();
        }
    }
})


// var n = 3;
// while (n != 1) {
//     if (n % 2 == 0) {
//         n = n / 2;
//         document.write(n+' ');
//     } else {
//         n = (3 * n) + 1;
//         document.write(n+' ');
//     }    
// } 
document.write('opa'.length);