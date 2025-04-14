const paragraphErrorCreateDataBase = document.getElementById("error-create-datadase");
const paragraphCreateDataBase = document.getElementById("messege-create-datadase");



const typeDataBase = document.getElementById("TypeDataBase");
const typeConnectDataBase = document.getElementById("TypeConnectDataBase");
const textTypeConnectDataBase = document.getElementById("text-TypeConnectDataBase");


SetColorTextError();
SetColorText();

SetComboBoxStyle();
ChekedChangeValueTypeDataBase();

function SetColorTextError() {
    if (paragraphErrorCreateDataBase.textContent != null) {
        paragraphErrorCreateDataBase.style.backgroundColor = '#CD5C5C';
        paragraphErrorCreateDataBase.style.borderRadius = '5px';
        paragraphErrorCreateDataBase.style.color = 'white';
    }
}

function SetColorText() {
    if (paragraphCreateDataBase.textContent != null) {
        paragraphCreateDataBase.style.backgroundColor = '#9DF583';
        paragraphCreateDataBase.style.borderRadius = '5px';
        paragraphCreateDataBase.style.color = '#000000';
    }
}

typeDataBase.addEventListener("change", () => {
    ChekedChangeValueTypeDataBase();
});


function ChekedChangeValueTypeDataBase() {
    if (typeDataBase.value == 1) {
        typeConnectDataBase.style.display = 'inline-block';
        textTypeConnectDataBase.style.display = 'inline-block';
    }
    else {
        typeConnectDataBase.style.display = 'none';
        textTypeConnectDataBase.style.display = 'none';
    }
}

function SetComboBoxStyle()
{
    typeConnectDataBase.style.display = 'none';
    textTypeConnectDataBase.style.display = 'none';
}