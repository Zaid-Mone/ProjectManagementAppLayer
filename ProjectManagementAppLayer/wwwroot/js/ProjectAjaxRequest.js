

// to get all paymentTerm based on project id
    function myFunction() {
    var optionValue = document.getElementById("mySelect").value; 
    $.ajax({
        type: "Get",
        url: `/ProjectManagment/PaymentTerm/GetprojectPayments?id=${optionValue}`,
        success: function (response) {
            for (var item of response.$values) {
                $('#secondDropdown').append(" <option value=" +item.id+ ">" + item.paymentTermTitle +" - "+ item.deliverable.description+"</option>")
                $('#secondDropdown option').addClass('submitButton');
            }
        },
        error: function (er) {
            console.log(er.responseText);
        }
        });
        console.clear();
    }


// to show the hide select list
$(document).ready(function () {
    $('#mySelect').change(function () {
        var selectedOption = $(this).val();
        
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






$(document).ready(function () {
    $('.submitButton').click(function () {
        var selectedValues = $.map($('#secondDropdown option:selected'), function (option) {
            return option.value;
        });
        console.clear()
        selectedValues.forEach(element => {
            $.ajax({
                type: "Get",
                url: `/ProjectManagment/PaymentTerm/GetPaymentSum?id=${element}`,
                success: function (response) {
                    console.log(response)
                },
                error: function (er) {
                    console.log(er.responseText);
                }
            });
        });
    });
});








// Get Single value 

//function GetSum() {
//    debugger
//    var optionValue = document.getElementById("secondDropdown").value;
//    console.log(optionValue);
//    $.ajax({
//        type: "Get",
//        url: `/ProjectManagment/PaymentTerm/GetPaymentSum?id=${optionValue}`,
//        success: function (response) {
//            console.log(response)
//        },
//        error: function (er) {
//            console.log(er.responseText);
//        }
//    });

//    console.clear();

//}