# Správa osobních financí a rozpočtu

## 1. Zadání projektu
Cílem projektu je vytvořit konzolovou aplikaci v jazyce C#, která uživateli umožní evidovat finanční příjmy a výdaje, zaznamenávat jejich popis a kategorie a sledovat celkový stav rozpočtu v čase.

### Funkce programu:
* **Správa transakcí:** Možnost přidat novou transakci (částka, datum, popis, kategorie, typ) a zobrazit kompletní barevně rozlišenou historii (příjmy zeleně, výdaje červeně).
* **Analýza dat:** Automatický výpočet a zobrazení aktuálního celkového zůstatku na základě všech evidovaných příjmů a výdajů.
* **Vizuální roční graf:** Možnost vykreslit přehledný sloupcový graf hospodaření pro vybraný rok v textové podobě ("kostičkách") přímo do konzole, který přehledně odděluje ziskové a ztrátové měsíce.
* **Ukládání a načítání dat:** Trvalé ukládání dat do textového souboru na disk, které zajišťuje, že uživatel nepřijde o svá data po zavření aplikace.

---

## 2. Jednoduchý model tříd včetně jejich vazeb
Aplikace důsledně využívá principy objektově orientovaného programování (OOP). Je rozdělena do 5 specializovaných tříd, které mají jasně vymezené odpovědnosti a vzámeně spolu komunikují.

### Schéma vazeb mezi třídami
```text
       ┌────────────────────────┐
       │        Program         │ 
       └───────┬───┬───┬────────┘
               │   │   │
     ┌─────────┘   │   └─────────┐
     ▼             ▼             ▼
┌─────────────┐ ┌─────────────┐ ┌────────────────────┐
│  Penezenka  │ │ GrafRender  │ │  SouborovyManager  │
└──────┬──────┘ └─────────────┘ └────────────────────┘
       │ 
       ▼
┌─────────────┐
│  Transakce  │ 
└─────────────┘
```

### Popis jednotlivých vazeb v kódu:
* **Třída `Program` (Controller):** Asociace směrem k třídám `Penezenka`, `SouborovyManager` a `GrafRender`. Inicializuje je a řídí tok dat na základě voleb v menu.
* **Vazba `Penezenka` -> `Transakce` (Agregace - 1:N):** Třída `Penezenka` v sobě zapouzdřuje dynamickou kolekci `List<Transakce> Historie`, nad kterou provádí matematické operace.
* **Vazba `GrafRender` -> `Penezenka` (Závislost):** Metoda `VykreslyGraf` přijímá objekt peněženky jako parametr a volá její metody pro zjištění měsíčních sum.
* **Vazba `SouborovyManager` -> `Transakce` (Závislost):** Třída přistupuje k vlastnostem transakcí při zápisu do textu a provádí instanciaci nových objektů při načítání ze souboru.

---

## 3. Struktura aplikace (třídy, metody)

### Třída: Program
* `static void Main(string[] args)` - Spouštěcí bod, obsahuje hlavní textové menu a cyklus programu.
* `static int Overeni_intu()` - Bezpečně načte celé číslo z konzole a ošetří překlepy uživatele přes `int.TryParse`.
* `static double Overeni_double()` - Bezpečně načte desetinné číslo (částka) z konzole přes `double.TryParse` a ošetří chybné vstupy.

### Třída: Transakce
* **Vlastnosti (Properties):** `Castka`, `Popis`, `Kategorie`, `JePrijem`, `Den`, `Mesic`, `Rok` (obsahují interní validaci rozsahu kalendářních dat v bloku `set`).
* `public Transakce(double castka, int den, int mesic, int rok, string popis, string kategorie, bool jeprijem)` - Konstruktor pro vytvoření a inicializaci objektu.
* `public void VypisDetail()` - Vytiskne kompletní detail transakce do konzole (příjmy zelenou barvou, výdaje červenou barvou).

### Třída: Penezenka
* **Kolekce:** `public List<Transakce> Historie` - Dynamická kolekce uchovávající transakce v paměti RAM.
* `public void PridatTransakci(Transakce novaTransakce)` - Přidá instanci transakce do seznamu.
* `public void VypisHistorii()` - Projede historii pomocí cyklu `foreach` a vypíše všechny detaily transakcí.
* `public double SpoctiZustatek()` - Spočítá celkovou finanční bilanci (příjmy sčítá, výdaje odečítá).
* `public double SpoctiMesicniPrijmy(int mesic, int rok)` - Filtruje historii a vrátí sumu příjmů za daný měsíc a rok.
* `public double SpoctiMesicniVydaje(int mesic, int rok)` - Filtruje historii a vrátí sumu výdajů za daný měsíc a rok.

### Třída: GrafRender
* `public void VykreslyGraf(Penezenka penezenka, int rok)` - Vyžádá si data z peněženky pro 12 měsíců, přepočítá bilance na kostičky (1 kostička = 1000 Kč) a vykreslí roční sloupcový graf.

### Třída: SouborovyManager
* `public void UlozData(string cestaKSouboru, List<Transakce> historie)` - Otevře soubor přes `StreamWriter` a zapíše data transakcí oddělená středníkem.
* `public List<Transakce> NactiData(string cestaKSouboru)` - Zkontroluje existenci souboru, čte jej přes `StreamReader`, rozděluje řádky metodou `.Split(';')` a parsuje data zpět do objektů.

---

## 4. Popis práce se soubory
Data aplikace jsou trvale ukládána do textového souboru `finance.txt` ve formátu podobném CSV, kde jsou jednotlivé hodnoty odděleny středníkem `;`. Jeden řádek textu reprezentuje jeden objekt transakce.

* **Zápis (Ukládání):** Třída `SouborovyManager` otevře soubor pomocí konstrukce `using` a třídy `StreamWriter`, cyklem `foreach` projde historii a zapíše data v pevné struktuře: `Částka;Den;Měsíc;Rok;Popis;Kategorie;JePrijem`.
* **Čtení (Načítání):** Při startu aplikace `SouborovyManager` ověří existenci souboru pomocí `File.Exists`. Pokud existuje, pomocí `StreamReader` čte řádky v cyklu `while`. Každý řádek metodou `.Split(';')` rozdělí na pole řetězců, převede (naparsuje) hodnoty pomocí `int.Parse`, `double.Parse` a `bool.Parse` na správné datové typy a obnoví objekty `Transakce` do paměti.

---

## 5. Popis ovládání
Program se kompletně ovládá textově přes konzolu zadáváním číselných voleb v hlavním menu:
1. **Přidat transakci:** Výzva k zadání parametrů transakce (částka, kalendářní den, měsíc, rok, textový popis, kategorie a typ transakce vyjádřený čísly: 1 = příjem / 0 = výdej).
2. **Vypsat historii:** Zobrazí chronologický barevný seznam všech transakcí z paměti.
3. **Zobrazit celkový zůstatek:** Ukáže aktuální celkový stav konta vyjádřený v Kč.
4. **Zobrazit roční graf:** Po zadání konkrétního roku vykreslí přehledný vizuální sloupcový graf (zelené kostičky rostou nahoru pro ziskové měsíce, červené klesají dolů pro ztrátové měsíce).
5. **Uložit a odejít:** Zapíše veškeré změny z operační paměti do souboru a bezpečně ukončí běžící aplikaci.
