# 📊 Temperature Analyzer – Object-Oriented Programming in C#

This project was created as part of **Object-Oriented Programming (OOP) in C#** classes.  
It is a simple console application that stores and analyzes temperatures (in **Kelvin**) for the last seven days for different cities.  
The program reads and saves data to a text file `cities.txt`.

---

## 📌 Features

✅ Store data for up to **15 cities**  
✅ Load temperature data from a text file  
✅ Display temperatures for all cities or for a selected city  
✅ Change temperature for a specific day in **Kelvin, Celsius, or Fahrenheit**  
✅ Add new cities with their temperature data  
✅ Perform basic temperature analysis: **average, minimum, maximum** (in Kelvin)  
✅ Save data back to the file in the same format as input  

---

## 🏗 Project structure

- **`Temperature`** – class that represents temperature data for a single city  
  - **Properties:**
    - `City` – name of the city  
    - `temperatures` – an array of 7 temperature values in Kelvin  
  - **Methods:**
    - `GetKelvin(day)`, `GetCelsius(day)`, `GetFahrenheit(day)` – read temperature in different units  
    - `SetKelvin(day, value)`, `SetCelsius(day, value)`, `SetFahrenheit(day, value)` – set temperature in different units  
    - `GetAverageTemperatureKelvin()`, `GetMinTemperatureKelvin()`, `GetMaxTemperatureKelvin()` – analyze temperatures  
    - `ToString()` – returns formatted string ready for saving into the file  

- **`Program`** – main class  
  - `Temperature[] cities` – array of up to 15 `Temperature` objects  
  - **Functions:**
    - `LoadFromFile()` – reads data from `cities.txt`  
    - `SaveToFile()` – saves data back to `cities.txt`  
    - `DisplayAllCities()` – displays all cities and their temperatures  
    - `DisplayCity()` – displays data for a selected city  
    - `ModifyTemperature()` – modifies a temperature for a specific day  
    - `AddCity()` – adds a new city to the list  
    - A console **menu** for easy navigation  

---

## 📂 Input file format (`cities.txt`)

The first line contains the number of cities.  
Each subsequent line has the following format:

