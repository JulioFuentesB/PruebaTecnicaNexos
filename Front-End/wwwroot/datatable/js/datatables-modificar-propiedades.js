// Call the dataTables jQuery plugin

///*tooltip*/
//$(function () {
//    $('[data-toggle="tooltip"]').tooltip()
//})


///*loading*/
//$(function () {

//    /*Muestra ventana de cargando cuando se hace un submit*/
//    $(document).on('submit', 'form', function () {
//        $('body').loading({
//            message: 'Procesando...'
//        });
//    });


//});

var tablaDataTable = null;
/*DataTable*/
$(function () {
    //para las tablas
    tablaDataTable = $('.tablaB').DataTable({
        language: {
            "decimal": "",
            "emptyTable": "No hay informacion",
            "info": "Mostrando _START_ a _END_ de _TOTAL_ Entradas",
            "infoEmpty": "Mostrando 0 to 0 of 0 Entradas",
            "infoFiltered": "(Filtrado de _MAX_ total entradas)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": "Mostrar _MENU_ Entradas",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "zeroRecords": "Sin resultados encontrados",
            "paginate": {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            }
        }
    });


    var data_table = $('.ordenaCodigo').DataTable();
    data_table.order([0, 'desc']).draw();

});



