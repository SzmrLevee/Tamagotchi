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

## Tesztelési Jegyzőkönyv

### Tesztelési Célok
- A játék alapvető funkcióinak ellenőrzése.
- A konzolos megjelenés megfelelő működésének tesztelése különböző képernyőméreteknél.
- A fiókkezelés és az állapotváltozások tesztelése.

### Tesztelési Esetek
| Teszt Azonosító | Funkció | Várt Eredmény | Tesztelési Eredmény |
|------------------|---------|---------------|---------------------|
| T1              | Főmenü  | Minden menüpont megjelenik és navigálható | Sikeres |
| T2              | Játék   | Animáció és menü egyidejű frissítése | Sikeres |
| T3              | Bolt    | Vásárlás során a pénz megfelelően csökken | Sikeres |
| T4              | Inventory | Az összegyűjtött tárgyak megjelenik | Sikeres |
| T5              | Fiókkezelés | Új fiók létrehozása és betöltése | Sikeres |

### MSTest Tesztek
- **PetTests.cs:** Az állapotváltozások ellenőrzése (éhség, fáradtság).
- **InventoryTests.cs:** Vásárlás és tárgyhasználat.
- **GameManagerTests.cs:** Menülogika tesztelése.

## Fejlesztői Dokumentáció

### Osztálystruktúra
#### GameManager
- **Felelősség:** A játék logikájának és állapotainak kezelése.
- **Metódusok:**
  - `LoadPet()`: Az állat adatait tölti be a fájlból.
  - `SavePet()`: Az állat adatait menti el.
  - `JatekMenu()`: A főmenü megjelenítése és navigáció.

#### Pet
- **Attribútumok:**
  - `Name`: Az állat neve.
  - `Health`: Életpontok.
  - `Hunger`: Éhség szintje.
  - `Tiredness`: Fáradtság szintje.
  - `Mood`: Hangulat pontok.
- **Metódusok:**
  - `Play()`: Az állat játszik.
  - `Sleep()`: Az állat alszik.

#### Inventory
- **Attribútumok:**
  - `Items`: A meglévő tárgyak listája.

### Adatszerkezetek

"Name": "Kisállatka",
"Health": 100,
"Hunger": 20,
"Tiredness": 30,
"Mood": 80,
"Inventory": [{"Name": "Étel", "Quantity": 5}]

# Tamagotchi Játék Dokumentáció

## Bevezetés
A Tamagotchi játék célja, hogy a felhasználó egy virtuális kisállatot gondozzon. A játék során különböző interakciókra van lehetőség, amelyek befolyásolják az állat éhségét, fáradtságát, hangulatát és egészségét.

---

## Felhasználói Útmutató

### Főmenü
A főmenüben navigálva az alábbi lehetőségek érhetők el:
1. **Játék** - Belépés a játékmenetbe.
2. **Bolt** - Tárgyak vásárlása az állat számára.
3. **Kisállat állapota** - Az aktuális statisztikák megtekintése.
4. **Interakciók** - Különböző akciók végrehajtása az állattal.
5. **Kilépés** - Kilépés a játékból.

### Játékmenet
A játékmenet során az alábbi opciók állnak rendelkezésre:
- **Játszik**: Az állat boldogságát növeli, de éhesebb és fáradtabb lesz.
- **Alszik**: Csökkenti a fáradtságot és növeli az egészséget.
- **Inventory**: Az állat tulajdonában lévő tárgyak megtekintése.
- **Vissza**: Visszatérés a főmenübe.

#### Gombok:
- `1`: Játszik
- `2`: Alszik
- `3`: Inventory
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

---

## Tesztelési Jegyzőkönyv

### Tesztelési Esetek

| Teszt Azonosító | Leírás                     | Elvárt Eredmény               | Teszteredmény |
|------------------|----------------------------|-------------------------------|---------------|
| T001            | Játék elindítása           | Játékmenet megfelelően indul | Sikeres       |
| T002            | Bolt funkció               | Vásárlás után a pénz csökken | Sikeres       |
| T003            | Kisállat állapota          | Az adatok pontosan megjelennek | Sikeres       |
| T004            | Interakciók végrehajtása   | Értékek frissülnek            | Sikeres       |
| T005            | Fiókkezelés                | Új fiók létrehozása működik  | Sikeres       |

---

## Fejlesztési Eszközök

- **Programozási nyelv:** C#
- **Könyvtárak:** System.Drawing, MSTest, System.Threading
- **IDE:** Visual Studio 2022

---

## Jövőbeli Fejlesztések
- GUI alapú megjelenítés.
- Multiplayer funkciók.
- Bővített inventory logika.
- Speciális események és küldetések hozzáadása.

---

## Adatszerkezetek

### Fájlformátum

"Name": "Kisállatka",
"Health": 100,
"Hunger": 10,
"Tiredness": 20,
"Mood": 80,
"Inventory": [
  { "Name": "Étel", "Quantity": 3 }
]

## Fejlesztési Eszközök

### Programozási Nyelv
- **C#**

### Könyvtárak
- **MSTest**: Tesztelési keretrendszer.
- **System.Drawing**: ASCII animációk megjelenítéséhez használt grafikus osztálykönyvtár.
- **System.Threading**: Szálkezelés az animációk és a program egyéb részei között.

### Fejlesztőkörnyezet (IDE)
- **Visual Studio 2022**
