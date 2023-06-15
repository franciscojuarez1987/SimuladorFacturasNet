function calcularTotal(input) {
    var index = input.getAttribute("data-index-precio");
    var precio = input.value;

    var cantidad = document.getElementById(`InvoiceModel_Elementos_${index}__Cantidad`).value;

    document.getElementById(`InvoiceModel_Elementos_${index}__Total`).value = cantidad && precio ? cantidad * precio : "";
} function eliminarProducto(index) {
    document.getElementById(`tr_producto_${index}`).remove();
}