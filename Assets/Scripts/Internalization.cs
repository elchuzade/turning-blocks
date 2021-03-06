using UnityEngine;
using static GlobalVariables;

public class Internalization : MonoBehaviour
{
    public string Translate(string key, Languages language)
    {
        switch (language)
        {
            case Languages.english:
                return GetEnglishWord(key);
            case Languages.russian:
                return GetRussianWord(key);
            case Languages.turkish:
                return GetTurkishWord(key);
            case Languages.spanish:
                return GetSpanishWord(key);
            case Languages.french:
                return GetFrenchWord(key);
            case Languages.german:
                return GetGermanWord(key);
        }
        return "";
    }

    /* ENGLISH */
    string GetEnglishWord(string key)
    {
        switch (key)
        {
            case "game modes":
                return "game modes";
            case "high score":
                return "high score";
            case "classical":
                return "classical";
            case "infinite":
                return "infinite";
            case "random":
                return "random";
            case "privacy policy":
                return "privacy policy";
            case "leaderboard":
                return "leaderboard";
            case "play":
                return "play";
            case "settings":
                return "settings";
            case "quit game":
                return "quit game";
            case "sounds":
                return "sounds";
            case "haptics":
                return "haptics";
            case "remove ads":
                return "remove ads";
            case "restore purchase":
                return "restore purchase";
            case "are you sure you want to quit":
                return "are you sure you want to quit";
            case "by tapping accept":
                return "by tapping accept you agree to our 'terms of use' and acknowledge our 'privacy policy'";
            case "terms of use":
                return "terms of use";
            case "change name":
                return "change name";
            case "type your name":
                return "type your name";
            case "name":
                return "name";
        }
        return "";
    }

    /* RUSSIAN */
    string GetRussianWord(string key)
    {
        switch (key)
        {
            case "game modes":
                return "???????????? ????????";
            case "high score":
                return "????????????";
            case "classical":
                return "????????????????????????";
            case "infinite":
                return "??????????????????????";
            case "random":
                return "??????????????????";
            case "privacy policy":
                return "";
            case "leaderboard":
                return "";
            case "play":
                return "";
            case "settings":
                return "";
            case "quit game":
                return "";
            case "sounds":
                return "";
            case "haptics":
                return "";
            case "remove ads":
                return "";
            case "restore purchase":
                return "";
            case "are you sure you want to quit":
                return "";
            case "by tapping accept":
                return "";
            case "terms of use":
                return "";
            case "change name":
                return "";
            case "type your name":
                return "";
            case "name":
                return "";
        }
        return "";
    }

    /* TURKISH */
    string GetTurkishWord(string key)
    {
        switch (key)
        {
            case "game modes":
                return "oyun modlar??";
            case "high score":
                return "y??ksek skor";
            case "classical":
                return "klasik";
            case "infinite":
                return "sonsuz";
            case "random":
                return "rastgele";
            case "privacy policy":
                return "gizlilik politikas??";
            case "leaderboard":
                return "liderler";
            case "play":
                return "oyna";
            case "settings":
                return "ayarlar";
            case "quit game":
                return "oyundan ????k";
            case "sounds":
                return "sesler";
            case "haptics":
                return "haptik";
            case "remove ads":
                return "reklamlari kald??r";
            case "restore purchase":
                return "sat??n almay?? geri y??kle";
            case "are you sure you want to quit":
                return "????kmak istedi??inden emin misin";
            case "by tapping accept":
                return "kabul et'e dokunarak 'kullan??m ko??ullar??m??z??' kabul ediyorsunuz ve 'gizlilik politikam??z??' kabul ediyorsunuz";
            case "terms of use":
                return "kullan??m ??artlar??";
            case "change name":
                return "ismini de??i??tir";
            case "type your name":
                return "ismini yaz";
            case "name":
                return "isim";
        }
        return "";
    }

    /* SPANISH */
    string GetSpanishWord(string key)
    {
        switch (key)
        {
            case "game modes":
                return "modos de juego";
            case "high score":
                return "puntuaci??n m??s alta";
            case "classical":
                return "cl??sico";
            case "infinite":
                return "infinito";
            case "random":
                return "aleatorio";
            case "privacy policy":
                return "pol??tica de privacidad";
            case "leaderboard":
                return "clasificaci??n";
            case "play":
                return "tocar";
            case "settings":
                return "ajustes";
            case "quit game":
                return "salir del juego";
            case "sounds":
                return "sonidos";
            case "haptics":
                return "h??ptica";
            case "remove ads":
                return "retirar anuncios";
            case "restore purchase":
                return "restaurar compra";
            case "are you sure you want to quit":
                return "seguro que quieres salir";
            case "by tapping accept":
                return "tocando aceptar acepta nuestros 't??rminos de uso' y reconoce nuestra 'pol??tica de privacidad'";
            case "terms of use":
                return "condiciones de uso";
            case "change name":
                return "cambiar nombre";
            case "type your name":
                return "escriba su nombre";
            case "name":
                return "nombre";
        }
        return "";
    }

    /* FRENCH */
    string GetFrenchWord(string key)
    {
        switch (key)
        {
            case "game modes":
                return "modes de jeu";
            case "high score":
                return "high score";
            case "classical":
                return "classique";
            case "infinite":
                return "infini";
            case "random":
                return "al??atoire";
            case "privacy policy":
                return "politique de confidentialit??";
            case "leaderboard":
                return "classement";
            case "play":
                return "jouer";
            case "settings":
                return "les param??tres";
            case "quit game":
                return "quitter le jeu";
            case "sounds":
                return "des sons";
            case "haptics":
                return "haptique";
            case "remove ads":
                return "supprimer les publicit??s";
            case "restore purchase":
                return "restaurer l'achat";
            case "are you sure you want to quit":
                return "??tes-vous s??r de vouloir quitter";
            case "by tapping accept":
                return "en appuyant sur accepter vous acceptez nos 'conditions d'utilisation' et reconnaissez notre 'politique de confidentialit??'";
            case "terms of use":
                return "conditions d'utilisation";
            case "change name":
                return "changer de nom";
            case "type your name":
                return "tapez votre nom";
            case "name":
                return "nom";
        }
        return "";
    }


    /* GERMAN */
    string GetGermanWord(string key)
    {
        switch (key)
        {
            case "game modes":
                return "spielmodi";
            case "high score":
                return "highscore";
            case "classical":
                return "klassisch";
            case "infinite":
                return "unendlich";
            case "random":
                return "zuf??llig";
            case "privacy policy":
                return "datenschutz bestimmungen";
            case "leaderboard":
                return "bestenliste";
            case "play":
                return "abspielen";
            case "settings":
                return "";
            case "quit game":
                return "spiel verlassen";
            case "sounds":
                return "ger??usche";
            case "haptics":
                return "haptik";
            case "remove ads":
                return "entfernen anzeigen";
            case "restore purchase":
                return "kauf wiederherstellen";
            case "are you sure you want to quit":
                return "sind Sie sicher, dass sie aufh??ren wollen";
            case "by tapping accept":
                return "indem du auf 'akzeptieren' tippst sie stimmen unseren 'nutzungsbedingungen' zu und erkennen unsere 'datenschutzerkl??rung' an";
            case "terms of use":
                return "nutzungsbedingungen";
            case "change name":
                return "namen ??ndern";
            case "type your name":
                return "gib deinen namen ein";
            case "name":
                return "name";
        }
        return "";
    }
}
