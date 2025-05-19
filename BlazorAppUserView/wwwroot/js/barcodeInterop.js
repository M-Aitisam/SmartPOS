// Print receipt function
window.printReceipt = function () {
    const printContent = document.getElementById('receiptToPrint').innerHTML;
    const originalContent = document.body.innerHTML;

    document.body.innerHTML = printContent;
    window.print();
    document.body.innerHTML = originalContent;
    window.location.reload();
};