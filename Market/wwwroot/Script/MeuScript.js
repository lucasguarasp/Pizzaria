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


//Get insumo
$(".editInsumo").click(function () {
    var dados = $(this).attr("rel").split("|");
    $("#EditNome").val(dados[0]);
    $("#EditQuantidade").val(dados[1]);
    $("#EditId").val(dados[2]);
});

//Post insumo
$(".salvar").click(function () {
    var id = $("#EditId").val();
    var nome = $("#EditNome").val();
    var quantidade = $("#EditQuantidade").val();
    var x = "";
    $.ajax({
        type: "POST",
        url: '/Administrativa/EditInsumo',
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

$(".editProdutoHasInsumo").click(function () {
    var id = $(this).attr("id");
    $.ajax({
        type: "POST",
        url: '/Administrativa/ProdutoHasInsumo',
        data: {
            'Id': id
        },
        success: function (data) {
            var insumo = "";
            $(data).each(function (i) {
                insumo += "<p>" + data[i].insumo.nome + "</p><input id='Insumo" + i + "' class='form-control' />";
            });

            $("#produtoaa").html(insumo);

            //alert('Enviado');
        },
        error: function (error) {
            alert('Nao enviou');
        }
    });
});

$('#selectInsumos').change(function () {
    var str = "";
    var id = "";   
   
    $("#selectInsumos option:selected").each(function (i) {
        id = $(this).text();
        str += $(this).text() + "<p><input type='text' name='Quantidade' id='Quantidade' class='form-control' placeholder='Quantidade do produto'/> </p>";
       
    });
    $("#Insumo").html(str);
    
})
    .trigger("change");

$(document).ready(function () {
    var teste = [];
    $("#Quantidade").each(function () {
        teste[$(this)] = $(this).val();
    });

    teste += $('#Quantidade').val() + ",";
    $("#Eviar").html(teste);
});

