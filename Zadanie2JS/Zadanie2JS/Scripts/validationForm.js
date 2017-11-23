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

function checkEmail(str) {
    if (isWhiteSpace(str)) {
        alert("Podaj właściwy e-mail");
        return false;
    }
    else {
        var at = str.indexOf("@");
        if (at < 1) {
            alert("Nieprawidłowy e-mail");
            return false;
        }
        else {
            var l = -1;
            for (var i = 0; i < str.length; i++) {
                var c = str.charAt(i);
                if (c == ".") {
                    l = i;
                }
            }
            if ((l < (at + 2)) || (l == str.length - 1)) {
                alert("Nieprawidłowy e-mail");
                return false;
            }
        }
        return true;
    }
}

function checkStringAndFocus(obj, msg) {
    var str = obj.value;
    var errorFieldName = "e_" + obj.name.substr(2, obj.name.length);
    if (isWhiteSpace(str) || isEmpty(str)) {
        document.getElementById(errorFieldName).innerHTML = msg;
        obj.focus();
        startTimer(errorFieldName);
        return false;
    }
    else {
        return true;
    }
}function startTimer(fName) {
    errorField = fName;
    window.setTimeout("clearError(errorField)", 5000);
}

function clearError(objName) {
    document.getElementById(objName).innerHTML = "";
}function showElement(e) {
    document.getElementById(e).style.visibility = 'visible';
}

function hideElement(e) {
    document.getElementById(e).style.visibility = "hidden";

}function checkEmailRegEx(str) {
    var email = /[a-zA-Z_0-9\.]+@[a-zA-Z_0-9\.]+\.[a-zA-Z][a-zA-Z]+/;
    if (email.test(str))
        return true;
    else {
        alert("Podaj właściwy e-mail");
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

}function validate(formularz) {
    var imie = formularz.elements["f_imie"];
    var nazwisko = formularz.elements["f_nazwisko"];
    var kod = formularz.elements["f_kod"];
    var street = formularz.elements["f_ulica"];
    var email = formularz.elements["f_email"];
    var city = formularz.elements["f_miasto"];


    if (checkStringAndFocus(imie, "Złe imię") && checkStringAndFocus(nazwisko, "ss") && checkStringAndFocus(kod, "ss")
        && checkStringAndFocus(street, "ss") && checkStringAndFocus(city, "ss") && checkStringAndFocus(email, "ss")
        && checkEmailRegEx(email)) {

        return true;
    }

    return false;
}

