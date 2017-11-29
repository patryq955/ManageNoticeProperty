var errorField = "";


function isEmpty(param1) {
    if (param1.length == 0) {
        return true;
    }

    return false;
}

function isWhiteSpace(str) {
    var ws = "\t\n\r ";
    for (var i = 0; i < str.length; i++) {
        var c = str.charAt(i);
        if (ws.indexOf(c) == -1) {
            return false;
        }
    }
    return true;
}

function checkString(str, message) {
    if (isEmpty(str) || isWhiteSpace(str)) {
        alert(message)
        return false;

    }
    return true;
}

function checkStringAndFocus(obj, msg) {
    var str = obj.value;
    var errorFieldName = "e_test";

    if (isWhiteSpace(str) || isEmpty(str)) {
        document.getElementById(errorFieldName).innerHTML += msg + "<br/>";
        startTimer(errorFieldName);
        obj.className = "wrong";

        return false;
    }
    else {
        return true;
    }
}function checkStringFocus(obj, name) {
    var str = obj.value;
    var errorFieldName = "e_" + name;

    if (isWhiteSpace(str) || isEmpty(str)) {
        document.getElementById(errorFieldName).innerHTML = "*";
        startTimer(errorFieldName);
        obj.className = "wrong";
        return false;
    }
    else {
        document.getElementById(errorFieldName).innerHTML = "";
        obj.classList.remove("wrong");

        return true;
    }
}function startTimer(fName) {
    errorField = fName;
    window.setTimeout("clearError(errorField)", 500000);
}

function clearError(objName) {
    document.getElementById(objName).innerHTML = "";
}function showElement(e) {
    document.getElementById(e).style.visibility = 'visible';
}

function hideElement(e) {
    document.getElementById(e).style.visibility = "hidden";

}function checkEmailRegEx(str, msg) {
    var errorFieldName = "e_test";
    var email = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (email.test(str.value))
        return true;
    else {
        document.getElementById(errorFieldName).innerHTML += msg + "<br/>";
        return false;
    }
}function checkEmailRegExFocus(obj) {
    var errorFieldName = "e_email";
    var email = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (email.test(obj.value)) {
        document.getElementById(errorFieldName).innerHTML = "";
        obj.classList.remove("wrong");
        return true;
    }
    else {
        document.getElementById(errorFieldName).innerHTML = "*";
        obj.className = "wrong";
        return false;
    }
}function checkZIPCodeRegEx(postCode) {
    var code = document.getElementById("kod");
    var x = document.getElementById("test").value;
    var pattern = /^[0-9]{2}-[0-9]{3}$/;

    if (pattern.test(x)) {
        code.innerHTML = "OK"
        code.className = "green"
        return true;
    }
    code.innerHTML = "Źle"
    code.className = "red"
    return false;

}function checkPassword(password, msg) {
    var errorFieldName = "e_test";
    var errorFieldName2 = "e_password";
    var pattern = /^(?=.*[0-9])(?=.*[!@#$%^&*])[a-zA-Z0-9!@#$%^&*]{8,16}$/;
    if (pattern.test(password.value)) {
        document.getElementById(errorFieldName2).innerHTML = "";
        password.classList.remove("wrong");
        return true;
    }
    if (msg != '') {
        document.getElementById(errorFieldName).innerHTML += msg + "<br/>";
    }
    password.className = "wrong";
    document.getElementById(errorFieldName2).innerHTML = "*";
    return false;
}function comparePassword(password, confirm, msg) {
    var errorFieldName2 = "e_passwordConfirm";
    if (password.value === confirm.value) {
        document.getElementById(errorFieldName2).innerHTML = "";
        confirm.classList.remove("wrong");
        return true;
    }
    confirm.className = "wrong";
    if (msg != '') {
        document.getElementById("e_test").innerHTML += msg + "<br/>";
    }
    return false;
}confirmfunction checkOpis(opis, msg) {
    var errorFieldName2 = "e_opis";
    if (opis.value.length <= 100) {
        document.getElementById(errorFieldName2).innerHTML = "";
        opis.classList.remove("wrong");
        return true;
    }
    var errorFieldName = "e_test";
    if (msg == '')
    {
        document.getElementById("e_test").innerHTML += msg + "<br/>";

    }
    opis.className = "wrong";
    document.getElementById(errorFieldName2).innerHTML = "*";
    return false;
}function validate(formularz) {
    document.getElementById("e_test").innerHTML = "";
    var imie = formularz.elements["f_imie"];
    var nazwisko = formularz.elements["f_nazwisko"];
    var email = formularz.elements["f_email"];
    var password = formularz.elements["f_password"];
    var opis = formularz.elements["f_opis"];
    var passwordConfirm = formularz.elements["f_passwordConfirm"];
    var star = document.getElementById("star");
    checkStringAndFocus(imie, "Podaj imię");
    checkStringAndFocus(nazwisko, "Podaj nazwisko");
    checkStringAndFocus(email, "podaj email");
    checkEmailRegEx(email, "Niepoprawny emal");
    checkPassword(password, "Hasło jest zbyt krótkie lub nie pasuje do wzorca");
    comparePassword(password, passwordConfirm, "Hasła nie są takie same");
    checkOpis(opis, "Opis jest za długi");

    if (checkOpis(opis, "") && checkStringAndFocus(imie, "") && checkStringAndFocus(nazwisko, "") && comparePassword(password, passwordConfirm)
        && checkEmailRegEx(email, "") && checkPassword(password, "")
        ) {

        return true;
    }
    return false;
}

