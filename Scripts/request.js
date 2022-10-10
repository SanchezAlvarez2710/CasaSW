const form = document.getElementById("form-request");
var select = document.getElementById('prodtype');

function clean() {
    form.reset();
}

function select() {
    if (select.options[select.selectedIndex].text == 'Other') {
        document.getElementById("pother").style.display = inline-block;
    }
}