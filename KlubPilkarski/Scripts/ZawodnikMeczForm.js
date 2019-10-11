var aktualnaKarta = 0;
wyswietlKarte(aktualnaKarta);

function wyswietlKarte(n) {
    var karty = document.getElementsByClassName("tab");
    var pozycje = document.getElementById("Pozycja");
    karty[n].style.display = "block";
    if (n == 0) {
        document.getElementById("poprzedni").style.display = "none";
    } else {
        document.getElementById("poprzedni").style.display = "inline";
    }
    if (n == (karty.length - 1)) {
        document.getElementById("nastepny").className.replace("btn btn-primary", "btn btn-success");
        document.getElementById("nastepny").innerHTML = "Dodaj";
    } else if (aktualnaKarta == 4 && pozycje.options[pozycje.selectedIndex].text == "BR") {
        document.getElementById("nastepny").className.replace("btn btn-primary", "btn btn-success");
        document.getElementById("nastepny").innerHTML = "Dodaj";
    } else {
        document.getElementById("nastepny").innerHTML = "Następne";
    }
    zmienZnacznikKroku(n);
}

function poprzedniNastepny(n) {
    var pozycje = document.getElementById("Pozycja");
    if (n == 1 && aktualnaKarta == 0 && pozycje.options[pozycje.selectedIndex].text != "BR") {
        n++;
    }
    if (n == -1 && aktualnaKarta == 2 && pozycje.options[pozycje.selectedIndex].text != "BR") {
        n--;
    }
    if (n == 1 && aktualnaKarta == 1 && pozycje.options[pozycje.selectedIndex].text == "BR") {
        n++;
    }
    if (n == -1 && aktualnaKarta == 3 && pozycje.options[pozycje.selectedIndex].text == "BR") {
        n--;
    }
    var karty = document.getElementsByClassName("tab");
    document.getElementsByClassName("krok")[aktualnaKarta].className += " zakonczony";
    karty[aktualnaKarta].style.display = "none";
    aktualnaKarta = aktualnaKarta + n;
    if (aktualnaKarta >= karty.length) {
        document.getElementById("zawodnikMeczForm").submit();
        return false;
    }
    if (aktualnaKarta == 5 && pozycje.options[pozycje.selectedIndex].text == "BR") {
        document.getElementById("zawodnikMeczForm").submit();
        return false;
    }
    wyswietlKarte(aktualnaKarta);
}

function zmienZnacznikKroku(n) {
    var i, kroki = document.getElementsByClassName("krok");
    var pozycje = document.getElementById("Pozycja");
    for (i = 0; i < kroki.length; i++) {
        kroki[i].className = kroki.className.replace("aktywny", "");
    }
    kroki[n].className += " aktywny";
}

function wyswietlZnacznikiKroku() {
    var i, kroki = document.getElementsByClassName("krok");
    var pozycje = document.getElementById("Pozycja"); 
    if (pozycje.options[pozycje.selectedIndex].text == "BR") {
        kroki[0].style.display = "inline-block";
        kroki[1].style.display = "inline-block";
        kroki[2].style.display = "none";
        kroki[3].style.display = "inline-block";
        kroki[4].style.display = "inline-block";
        kroki[5].style.display = "none";
        kroki[6].style.display = "none";
        kroki[7].style.display = "none";
    } else {
        kroki[0].style.display = "inline-block";
        kroki[1].style.display = "none";
        kroki[2].style.display = "inline-block";
        kroki[3].style.display = "inline-block";
        kroki[4].style.display = "inline-block";
        kroki[5].style.display = "inline-block";
        kroki[6].style.display = "inline-block";
        kroki[7].style.display = "inline-block";
    }
}

function ustawPozycje(pozycja) {
    var i, pozycje = document.getElementById("Pozycja");
    for (i = 0; i < 13; i++) {
        if (pozycje.options[i].text == pozycja) {
            pozycje.options[i].selected = true;
        }
    }
    wyswietlZnacznikiKroku();
}