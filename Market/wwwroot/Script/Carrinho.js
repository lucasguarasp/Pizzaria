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
        CheckoutView();
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
    var d = new Date();
    d.setTime(d.getTime() + (15 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    
    //15 minutos cookie
    document.cookie = 'Projeto=' + encodeURIComponent((JSON.stringify(produtos))) + ';Expires=' + expires;

};

//chama controoler e envia array de produtos
function Checkout() {
    // Pega Cookie com nome "Projeto"
    var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    var dec = decodeURIComponent(cookieValue);
    if (dec != "") {
        var arrayProdutos = $.parseJSON(dec);
    }

    var finaliza = document.querySelector("#finaliza");
    if (finaliza !== null) {
        finaliza = finaliza.getAttribute("value");
        deleteCookie("Projeto");
    }

    //alert(finaliza);
    $.ajax({
        type: "POST",
        url: '/Checkout/Checkout',
        dataType: 'json',
        async: false,
        data: {
            'produtos': arrayProdutos,
            'finaliza': finaliza
        },
        success: function (data) {
            window.location = "/Checkout/Checkout";
            produtos.forEach(function (produto, i) {
                produto["Imagem"] = data[i].imagem.replace("~", "");
                produto["Descricao"] = data[i].descricao;
                setCookie();
            });
        },
        error: function (error) {

        }
    });
}

$(document).ready(function () {
    if (window.location.pathname === "/Checkout/Checkout") {
        CheckoutView();
    }

    //adiciona itens no carrinho se existir cookie
    if (window.location.pathname === "/Checkout/Index") {

        var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");

        var dec = decodeURIComponent(cookieValue);
        if (cookieValue != "") {
            produtos = $.parseJSON(dec);
            ExibirCarrinho();
        }
    }
})

function deleteCookie(cname) {
    var d = new Date(); //Create an date object
    d.setTime(d.getTime() - (1000 * 60 * 60 * 24)); //Set the time to the past. 1000 milliseonds = 1 second
    var expires = "expires=" + d.toGMTString(); //Compose the expirartion date
    window.document.cookie = cname + "=" + "; " + expires;//Set the cookie with name and the expiration date

}

function CheckoutView() {
    var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");

    var dec = decodeURIComponent(cookieValue);
    if (cookieValue != "") {
        produtos = $.parseJSON(dec);
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
        ItemProduto += '<div class="col-sm-2 hidden-xs"><img src="' + produtos[i].Imagem + '" alt="..." class="img-responsive"/></div>';
        ItemProduto += '<div class="col-sm-10">';
        ItemProduto += '<h4 class="nomargin">' + produtos[i].Nome + '</h4>';
        ItemProduto += '<p>' + produtos[i].Descricao + '</p>';
        ItemProduto += '</div>';
        ItemProduto += '</div>';
        ItemProduto += '</td>';
        ItemProduto += '<td data-th="Price">R$ ' + produtos[i].Valor + '</td>';
        ItemProduto += '<td data-th="Quantity">';
        ItemProduto += '<input type="number" class="form-control text-center" min="0" value="' + produtos[i].Quantidade + '" id="' + produtos[i].Id + '" onBlur="AlterarQtd(' + produtos[i].Id + ',this.value);">';
        ItemProduto += '</td>';
        ItemProduto += '<td class="text-center">' + (produtos[i].Valor * produtos[i].Quantidade).toFixed(2) + '</td>';
        ItemProduto += '<td class="actions pull-right" data-th="">';
        ItemProduto += '<button class="btn btn-danger btn-sm" onclick="RemoveAll(' + produtos[i].Id + ')"><i class="fa fa-trash-o"></i></button>';
        ItemProduto += '</td>';
        ItemProduto += '</tr>';
    });

    $("#produto").html(ItemProduto);
    $("#total").html("R$ " + total.toFixed(2));

}

function AlterarQtd(id, value) {
    var cookieValue = document.cookie.replace(/(?:(?:^|.*;\s*)Projeto\s*\=\s*([^;]*).*$)|^.*$/, "$1");
    var dec = decodeURIComponent(cookieValue);
    if (cookieValue != "") {
        produtos = $.parseJSON(dec);

        let index = produtos.findIndex(x => x.Id == id);
        produtos[index].Quantidade = parseInt(value);

        if (value <= 0) {
            produtos.splice(index, 1);
            setCookie();
            CheckoutView();
        } else {
            setCookie();
            CheckoutView();
        }


    }
}




//expira em 15 min
//document.cookie = "Gabriel=Chupa Meu Pau" + ";Expires=" + ExpireMin;
// document.cookie = "Rodrigo=Viado" + ";Expires=Wed, 30 Aug 2019 00:00:00";
//document.cookie = "Teste=" + '[{"Id":20,"Nome":"Prato Feito","Valor":12.5,"Quantidade":1,"Imagem":"/images/ImgEmpty/noImg.jpg","Descricao":"Batata Palha, Cebola, Tomate"}]';









