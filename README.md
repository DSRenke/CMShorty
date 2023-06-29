# CM Shorty

## ## Projektbeschreibung

Beispiel Umsetzung der Funktion Short-URL als Xamarin App. Unter Verwendung des Service short.io [API-Doku](https://developers.short.io/reference/apilinksget)

## Verwendung
Für die Verwendung der short.io-API wird ein Autorisierungstoken und eine Domain aus dem Backend von short.io benötigt. Diese müssen in der [AppConfig.json](https://github.com/DSRenke/CMShorty/blob/main/CMShorty/CMShorty/AppConfig.json)-Datei angegeben werden. eingetragen werden.

CMShorty/CMShorty/AppConfig.json
``` json
{
  "ShortIO": {
    "Authorization": "key",
    "Domain": "domain"
  }
}
```