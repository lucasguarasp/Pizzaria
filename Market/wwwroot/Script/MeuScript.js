$(function () {
    //Initialize Select2 Elements
    $('.select2').select2();

    //Datemask dd/mm/yyyy
    $('#datemask').inputmask('dd/mm/yyyy', { 'placeholder': 'dd/mm/yyyy' });
    //Datemask2 mm/dd/yyyy
    $('#datemask2').inputmask('mm/dd/yyyy', { 'placeholder': 'mm/dd/yyyy' });
    //Money Euro
    $('[data-mask]').inputmask();

    //Date range picker
    $('#reservation').daterangepicker();
    //Date range picker with time picker
    $('#reservationtime').daterangepicker({ timePicker: true, timePickerIncrement: 30, format: 'MM/DD/YYYY h:mm A' });
    //Date range as a button
    $('#daterange-btn').daterangepicker(
        {
            ranges: {
                'Today': [moment(), moment()],
                'Yesterday': [moment().subtract(1, 'days'), moment().subtract(1, 'days')],
                'Last 7 Days': [moment().subtract(6, 'days'), moment()],
                'Last 30 Days': [moment().subtract(29, 'days'), moment()],
                'This Month': [moment().startOf('month'), moment().endOf('month')],
                'Last Month': [moment().subtract(1, 'month').startOf('month'), moment().subtract(1, 'month').endOf('month')]
            },
            startDate: moment().subtract(29, 'days'),
            endDate: moment()
        },
        function (start, end) {
            $('#daterange-btn span').html(start.format('MMMM D, YYYY') + ' - ' + end.format('MMMM D, YYYY'));
        }
    );

    //Date picker
    $('#datepicker').datepicker({
        autoclose: true
    });

    //iCheck for checkbox and radio inputs
    $('input[type="checkbox"].minimal, input[type="radio"].minimal').iCheck({
        checkboxClass: 'icheckbox_minimal-blue',
        radioClass: 'iradio_minimal-blue'
    });
    //Red color scheme for iCheck
    $('input[type="checkbox"].minimal-red, input[type="radio"].minimal-red').iCheck({
        checkboxClass: 'icheckbox_minimal-red',
        radioClass: 'iradio_minimal-red'
    });
    //Flat red color scheme for iCheck
    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
        checkboxClass: 'icheckbox_flat-green',
        radioClass: 'iradio_flat-green'
    });

    //Colorpicker
    $('.my-colorpicker1').colorpicker();
    //color picker with addon
    $('.my-colorpicker2').colorpicker();

    //Timepicker
    $('.timepicker').timepicker({
        showInputs: false
    });

    //Dinheiro reais
    $(".ValorReais").maskMoney({ prefix: '', allowNegative: false, thousands: ',', decimal: ',', affixesStay: false });

});

/*
 A função Mascara tera como valores no argumento os dados inseridos no input (ou no evento onkeypress)
 onkeypress="mascara(this, '## ####-####')"
 onkeypress = chama uma função quando uma tecla é pressionada, no exemplo acima, chama a função mascara e define os valores do argumento na função
 O primeiro valor é o this, é o Apontador/Indicador da Mascara, o '## ####-####' é o modelo / formato da mascara
 no exemplo acima o # indica os números, e o - (hifen) o caracter que será inserido entre os números, ou seja, no exemplo acima o telefone ficara assim: 11-4000-3562
 para o celular de são paulo o modelo deverá ser assim: '## #####-####' [11 98563-1254]
 para o RG '##.###.###.# [40.123.456.7]
 para o CPF '###.###.###.##' [789.456.123.10]
 Ou seja esta mascara tem como objetivo inserir o hifen ou espaço automáticamente quando o usuário inserir o número do celular, cpf, rg, etc

 lembrando que o hifen ou qualquer outro caracter é contado tambem, como: 11-4561-6543 temos 10 números e 2 hifens, por isso o valor de maxlength será 12
 <input type="text" name="telefone" onkeypress="mascara(this, '## ####-####')" maxlength="12">
 neste código não é possivel inserir () ou [], apenas . (ponto), - (hifén) ou espaço
 */


function readURL(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function(e) {
            $('#imagePreview').css('background-image', 'url('+e.target.result +')');
            $('#imagePreview').hide();
            $('#imagePreview').fadeIn(650);
        }
        reader.readAsDataURL(input.files[0]);
    }
}
$("#imageUpload").change(function() {
    readURL(this);
});

//mascara de cep
function CEP(t, mask) {
    var i = t.value.length;
    var saida = mask.substring(1, 0);
    var texto = mask.substring(i);
    if (texto.substring(0, 1) !== saida) {
        t.value += texto.substring(0, 1);
    }
}

//mascara de CPF
function CPF(t, mask) {
    var i = t.value.length;
    var saida = mask.substring(1, 0);
    var texto = mask.substring(i);
    if (texto.substring(0, 1) !== saida) {
        t.value += texto.substring(0, 1);
    }
}

//mascara de CEL
function CEL(o, f) {
    v_obj = o;
    v_fun = f;
    setTimeout("execmascara()", 1);
}
function mcel(v) {
    v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}

//mascara de TEL
function TEL(o, f) {
    v_obj = o;
    v_fun = f;
    setTimeout("execmascara()", 1);
}
function mtel(v) {
    v = v.replace(/\D/g, "");             //Remove tudo o que não é dígito
    v = v.replace(/^(\d{2})(\d)/g, "($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v = v.replace(/(\d)(\d{4})$/, "$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}

//exculta mascara tel e cel
function execmascara() {
    v_obj.value = v_fun(v_obj.value);
}

//Tabela
$(function () {
    $('#example1').DataTable();
    $('#example2').DataTable({
        'paging': true,
        'lengthChange': false,
        'searching': false,
        'ordering': true,
        'info': true,
        'autoWidth': false
    });
});

$(document).ready(function() {

    
    var readURL = function(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                $('.profile-pic').attr('src', e.target.result);
            }
    
            reader.readAsDataURL(input.files[0]);
        }
    }
    

    $(".file-upload").on('change', function(){
        readURL(this);
    });
    
    $(".upload-button").on('click', function() {
       $(".file-upload").click();
    });
});

//Get insumo
$(".editInsumo").click(function () {
    var dados = $(this).attr("rel").split("|");
    $("#EditNome").val(dados[0]);
    $("#EditQuantidade").val(dados[1]);
    $("#EditId").val(dados[2]);

    //toda vez que clicar para editar insumo, remove todas options.
    $('#insumo').find('option').remove().end();

    //pega id do insumo e manda para o controller, e retorna para trazer as categorias
    var id = $("#EditId").val();

    $.ajax({
        url: '/Produto/ShowInsumos',
        type: 'GET',
        dataType: 'json',
        data: {
            'EditId': id
        },
        success: function (data) {
            var itens = "";
            var option = "";
            $.each(data, function (i) {
                itens = data[i].categoria.descricao;
                option += $(this).text() + '<option value=' + itens + ' selected="selected">' + itens + '</option>';
            });
            $('#insumo').append(option);
        }

    });

});

//Post insumo
$(".salvar").click(function () {
    var id = $("#EditId").val();
    var nome = $("#EditNome").val();
    var quantidade = $("#EditQuantidade").val();
    var x = "";
    $.ajax({
        type: "POST",
        url: '/Produto/EditInsumo',
        data: {
            'Id': id,
            'Nome': nome,
            'Quantidade': quantidade
        },
        success: function (data) {
            alert('Enviado');
        },
        error: function (error) {
            alert('Nao enviou');
        }
    });
});


//Get insumos do produto
$(".editProdutoHasInsumo").click(function () {
    var id = $(this).attr("id");

    //Pega Nome e Caminho da foto do produto
    var dados = $(this).attr("rel").split("|");

    $.ajax({
        type: "POST",
        url: '/Produto/ProdutoHasInsumo',
        data: {
            'Id': id
        },
        success: function (data) {

            //coloca o nome do produto no titulo da modal           
            $("#title").html(dados[0]);
            $("#foto").html("<img src='" + dados[1].replace("~", "") + "' class='direct-chat-img' max-width='65px !important' max-height='65px !important'/>");
            $("#valor").html("<div class='col-sm-4'><label class='control-label'>Valor</label>" + "<input class='form-control' type='number' value='" + dados[2].replace(",", ".") + "'" + dados[2] + "/></div>");
            $("#categoriaP").html("<div class='col-sm-4'><label class='control-label'>Categoria</label>" + "<select class='form-control select2' style='width: 100 %;'><option selected='selected' value='" + dados[3] + "'> " + dados[3] + " </option></select></div>");
            $("#medida").html("<div class='col-sm-4'><label class='control-label'>Medida</label>" + "<select class='form-control select2' style='width: 100 %;'><option selected='selected' value='" + dados[4] + "'> " + dados[4] + " </option></select></div>");


            var insumo = "";
            $(data).each(function (i) {
                //cria input para cada insumo do produto
                insumo += "<div class='col-sm-3'><label class='control-label'>" + data[i].insumo.nome + "</label>" + "<input id=" + data[i].idProdutoHasInsumo + " class='form-control' type='number'/></div>";

                //depois de colocar o input acima, insere o valor da quantidade no input
                $(document).ready(function () {
                    $("#" + data[i].idProdutoHasInsumo + "").val(data[i].quantidade);
                });
            });
            $("#insumos").html(insumo);
        },
        error: function (error) {
            alert('Nao enviou');
        }
    });
});

//Coloca inputs para insumos selecionados em adicionar produto
var itens;
$('#selectInsumos').change(function () {
    var str = "";
    var id = "";

    $("#selectInsumos option:selected").each(function (i) {
        id = "Quantidade" + i + "";
        str += "<div class='col-sm-3'><label class='control-label'>" + $(this).text() + "</label> " + "<input type='text'  name='" + id + "' id='" + id + "' class='form-control ' onblur='myFunction(" + id + ", " + i + ")'  placeholder='Quantidade do produto'/></div>";
        itens = new Array(i);
    });
    $("#Insumo").html(str);

});

//salva array dos inputs dos insumos e colocar dentro de um select(Quantidade)
function myFunction(recebe, i) {
    if (recebe.value !== "") {
        itens[i] = "<option selected value= " + recebe.value + "> " + " </option>";
    } else {
        itens.value = "";
    }
    $(document).ready(function () {
        $("#Quantidade").html(itens);
    });
}

//retorna as medidas das categorias
$("#categoria").change(function () {
    //depois de escolher categorias, limpa insumos
    $('#Insumo').html("");

    var categoria = $("#categoria").val();
    $.ajax({
        url: '/Produto/AddProduto',
        type: 'GET',
        dataType: 'json',
        data: {
            'cat': categoria
        },
        success: function (data) {
            var itens = "<option>Selecione Medida</option>";
            var option = "";
            $.each(data.data1, function (i) {
                itens += "<option value= " + data.data1[i].idMedida + "> " + data.data1[i].nome + " </option>";

            });
            $("#medida").html(itens);

            $.each(data.data2, function (i) {
                option += $(this).text() + '<option value=' + "\'" + data.data2[i].insumo.nome + "\'" + '>' + data.data2[i].insumo.nome + '</option>';
            });
            $('#selectInsumos').html(option);


        },
        error: function (error) {
            $("#medida").html("<option>Selecione Medida</option>");
        }

    });
});