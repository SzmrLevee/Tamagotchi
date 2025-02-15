# Tamagotchi Játék Dokumentáció

## Bevezetés
A Tamagotchi játék célja, hogy a felhasználó egy virtuális kisállatot gondozzon. A játék során különböző interakciókra van lehetőség, amelyek befolyásolják az állat éhségét, fáradtságát, hangulatát és egészségét.

---

## Felhasználói Dokumentáció

### Általános Információk
Ez a Tamagotchi típusú játék egy virtuális kisállat gondozását teszi lehetővé. A játék célja, hogy minél tovább életben tartsuk a kisállatot, miközben állapota és hangulata a lehető legjobb marad.

#### Funkciók:
- **Játék:** Az állat boldogságát növeli, de fáradtabb lesz.
- **Alvás:** Csökkenti a fáradtságot és növeli az egészséget.
- **Inventory:** Az összegyűjtött tárgyak megtekintése és használata.
- **Bolt:** Tárgyak vásárlása az állat boldogságának és egészségének javítása érdekében.

#### Állapotok és Akciók
- **Élet:** Ha az élet 0-ra csökken, a játék véget ér.
- **Éhség:** Rendszeres etetéssel csökkenthető.
- **Fáradtság:** Alvással csökkenthető.
- **Hangulat:** Játékkal növelhető.

##### Gombok:
- `1`: Játszik
- `2`: Alszik
- `3`: Inventory megtekintése
- `4`: Vissza a főmenübe
- `Esc`: Főmenübe lépés bármelyik almenüből.

---

## Fejlesztői Dokumentáció

### Osztályok és Felelősségek

#### GameManager
- **Felelősség:** A játék logikájának kezelése.
- **Fő metódusok:**
  - `LoadPet()`: Az állat betöltése.
  - `SavePet()`: Az állat adatainak mentése.
  - `JatekMenu()`: A főmenü kezelése.

#### Pet
- **Attribútumok:**
  - `Name`: Az állat neve.
  - `Health`: Az állat élete.
  - `Hunger`: Az állat éhsége.
  - `Tiredness`: Az állat fáradtsága.
  - `Mood`: Az állat hangulata.
- **Fő metódusok:**
  - `Play()`: Játék az állattal.
  - `Sleep()`: Alvás.

#### Inventory
- **Attribútumok:**
  - `Items`: Tárgyak listája.
- **Fő metódusok:**
  - `AddItem()`: Új tárgy hozzáadása.

#### FiokKezeles
- **Felelősség:** A fiókok kezeléséért felelős osztály, amely biztosítja a fiókadatok tárolását és módosítását fájlokban.
- **Fő metódusok:**
  - `GetSettingsPath()`: Az alapértelmezett beállítások fájlútvonalát adja vissza.
  - `Novelem()`: Az adott elem mennyiségének növelése egy fiókban.

#### AsciiAnimation
- **Felelősség:** ASCII animációk megjelenítését biztosítja.
- **Fő metódusok:**
  - `RunAnimation()`: Animáció futtatása egy megadott név alapján.

#### IdoMulas
- **Felelősség:** Rendszeres időközönként frissíti a kisállat állapotát (éhség, fáradtság stb.).
- **Fő metódusok:**
  - `FrissitTulajdonsagok()`: Az állapotok időalapú frissítése (éhség, fáradtság növelése, élet csökkentése kritikus szinteken).
  - `Stop()`: Az időzítő leállítása.

#### Mentes
- **Felelősség:** A játékállapot mentéséért és betöltéséért felel.
- **Fő metódusok:**
  - `UjFiok()`: Új fiók létrehozása.
  - `Fomenu()`: A főmenü megjelenítése.

#### JatekmenetView
- **Felelősség:** A játék logikáját és vizuális elemeit kezeli.
- **Fő metódusok:**
  - `MainFuttatasa()`: A játék fő folyamatának futtatása.
  - `RunAnimationLoop()`: Animációs ciklus vezérlése.

#### ShopMenu
- **Felelősség:** A bolt működését kezeli.
- **Fő metódusok:**
  - `DisplayShopMenu()`: A bolt menüjének megjelenítése.
  - `ExecutePurchase()`: Vásárlási folyamat lebonyolítása.

#### RandomInterakciok
- **Felelősség:** A kisállat életét színesítő véletlen eseményekért, mint a betegség vagy sérülés.
- **Fő metódusok:**
  - `RandomInteraction()`: Véletlenszerű esemény generálása a kisállattal.

#### FileManager
- **Felelősség:** JSON fájl alapú adatmentésért és visszatöltésért felel.
- **Fő metódusok:**
  - `LoadData<T>()`: Adatok betöltése JSON fájlból.
  - `SaveData<T>()`: Adatok mentése JSON fájlba.

---

## Tesztelési Jegyzőkönyv

### Tesztelési Célok
- A játék alapvető funkcióinak ellenőrzése.
- A konzolos megjelenés megfelelő működésének tesztelése különböző képernyőméreteknél.
- A fiókkezelés és az állapotváltozások tesztelése.

### Tesztelési Esetek
| Teszt Azonosító | Funkció              | Várt Eredmény                                      | Tesztelési Eredmény |
|------------------|----------------------|--------------------------------------------------|---------------------|
| T1              | Főmenü               | Minden menüpont megjelenik és navigálható         | Sikeres             |
| T2              | Játék                | Animáció és menü egyidejű frissítése              | Sikeres             |
| T3              | Bolt                 | Vásárlás során a pénz megfelelően csökken         | Sikeres             |
| T4              | Inventory            | Az összegyűjtött tárgyak megjelennek             | Sikeres             |
| T5              | Fiókkezelés          | Új fiók létrehozása és betöltése                 | Sikeres             |

### MSTest Tesztek
- **PetTests.cs:** Az állapotváltozások ellenőrzése (éhség, fáradtság).
- **InventoryTests.cs:** Vásárlás és tárgyhasználat.
- **GameManagerTests.cs:** Menülogika tesztelése.

---

### Programozási Nyelv
- **C#**

### Könyvtárak
- **MSTest**: Tesztelési keretrendszer.
- **System.Drawing**: ASCII animációk megjelenítéséhez használt grafikus osztálykönyvtár.
- **System.Threading**: Szálkezelés az animációk és a program egyéb részei között.

### Fejlesztőkörnyezet (IDE)
- **Visual Studio 2022**
