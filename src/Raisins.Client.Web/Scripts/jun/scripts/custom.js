var ptype = document.getElementById('ClassID');
var curr = document.getElementById('CurrencyID');
var btntext = document.getElementById('submit');
var curtab = 0; $(document).ready(function () { SetSelection(curtab); });
var op;
if (retOps()) {
    op = "UPDATE";
}
else {op = "CREATE" }

$("#tabs").tabs();

function SetSelection(x) {
    ptype.value = curtab = x;
    if (x == 0) {
        curr.disabled = true;
        curr.value = 1;
        btntext.value = op + " LOCAL";
    }
    else if (x == 1) {
        curr.disabled = false;
        curr.value = 1;
        btntext.value = op + " EXTERNAL";
    }
    else {
        curr.disabled = false;
        curr.value = 1;
        btntext.value = op + " FOREIGN";
    }
}

function EnableDropDownBeforeSubmission() {
 
    curr.disabled = false;
}