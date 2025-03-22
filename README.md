# ✈️ AirlineManagement

A console-based airline staff and flight management system written in C#. Built with object-oriented design in mind.

Flights require one captain, one co-pilot, and exactly three flight attendants. Additional constraints include language coverage for the flight and possession of navigational tools (like a compass) by the captain.

The project is designed with strong object-oriented principles, making it a great exercise in modeling real-world entities like staff and flights in an aviation setting.

## 📚 Overview

The application allows users to:

- Add pilots (with captain status)
- Add flight attendants (with languages spoken)
- Create a flight by assigning:
  - One captain
  - One co-pilot
  - Three flight attendants
  - A flight ID and working language
- Check if a flight is ready for takeoff based on crew and language criteria

## 🧰 Technologies

- Language: **C#**
- Type: **.NET Console Application**
- Architecture: **Object-Oriented Programming (OOP)**

- ## 🛠️ Menu Options

| Option | Description |
|--------|-------------|
| 1      | Add a new pilot |
| 2      | Add a flight attendant |
| 3      | Create a flight |
| 4      | Check if a flight is ready for takeoff |
| 5      | Exit the application |

## ✈️ Flight Readiness Logic

A flight is considered **ready for takeoff** if:

- A captain and co-pilot are assigned
- There are exactly 3 flight attendants assigned
- The captain has a compass
- At least one flight attendant speaks the flight language

## 💡 Features

- Full console interaction
- Validations for inputs like names, dates, languages
- Smart selection menus for assigning pilots and crew
- Rich feedback in the console after every action

## 📦 How to Run

1. Clone the repository:
   ```bash
   git clone https://github.com/aleTomasz/AirlineManagement.git
