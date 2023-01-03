
    function myFunction() {
    var optionValue = document.getElementById("mySelect").value;
    debugger;
    $.ajax({
        type: "Get",
        url: `/ProjectManagment/PaymentTerm/GetprojectPayments?id=${optionValue}`,
        success: function (response) {
                //debugger;
            //alert(`Check Console id-: ${optionValue}`);$values
            console.log(response);
        for (var item of response.$values) {
        console.log(item.paymentTermTitle)
            //$('#secondDropdown').append(" <option value=" + item.id + ">" + item.paymentTermTitle +"</option>")
            $('#secondDropdown').append(" <option value=" + item.id + ">" + item.paymentTermTitle +" - "+ item.deliverable.description+"</option>")
                }
        },
        error: function (er) {
            console.log(er.responseText);
        }
        });
        
        console.clear();
    }



$(document).ready(function () {
    $('#mySelect').change(function () {
        var selectedOption = $(this).val();
        debugger;
        if (selectedOption != null) {
            // Show the second dropdown list
            $('#secondDropdown').show();
            $('#dropdownLabel').show();
        } else {
            // Hide the second dropdown list
            $('#secondDropdown').hide();
            $('#dropdownLabel').hide();
        }
    });
});