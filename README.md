# 🏨 Hotel Management System

> A system that runs a hotel's daily work: rooms, bookings, offers, guests and staff permissions.

---

## 🧐 What is this, in plain words?

A hotel has to answer many questions every day:
*Which rooms are free next weekend? Who booked room 204? Is there a discount this month? Which employee is allowed to cancel a booking?*

This project is the **back-end (the "brain")** of a hotel system that keeps track of all that:

- 🛏️ **Rooms** — types, facilities (Wi-Fi, sea view, ...), connected rooms.
- 📅 **Reservations** — book one or more rooms, see who is staying when.
- 🏷️ **Offers** — special prices and discounts on selected rooms.
- 💳 **Payments** for reservations.
- ⭐ **Reviews** and **favorite rooms** for guests.
- 🛡️ **Roles & permissions** — decide exactly what each employee can do (e.g. reception can book, only managers can delete).

## 👥 Who uses it?

| Who | What they can do |
|---|---|
| **Guest / Customer** | Browse rooms and offers, book, pay, review, save favorites |
| **Reception / Staff** | Manage reservations and guests (based on their permissions) |
| **Admin / Manager** | Manage rooms, offers, staff, and which features each role can access |

## 🔄 How it works (the simple story)

```
Guest browses rooms & offers ➜ Books a room ➜ Pays ➜ Staff confirm/manage the stay ➜ Guest leaves a review
```

---

## 🛠️ For developers

**Tech stack:** ASP.NET Core Web API (.NET 9) · Layered architecture (Core / Services / Infrastructure / API) · Entity Framework Core + SQL Server · AutoMapper · Serilog · Role–feature based authorization · Swagger

**Project structure**

| Folder | Role |
|---|---|
| `HotelArchitecture.Core/` | Entities: Room, Reservation, Offer, Payment, Review, Role, Feature, Staff, ... |
| `Hotel.Services/` | Business logic |
| `Hotel.Infrastructure/` | Database access (EF Core) |
| `HotelSystem/` | The Web API (controllers, startup) |

**Run locally**

1. Install the [.NET 9 SDK](https://dotnet.microsoft.com/download) and SQL Server.
2. Set your connection string in `HotelSystem/appsettings.json`.
3. Run:
   ```bash
   dotnet ef database update --project Hotel.Infrastructure --startup-project HotelSystem
   dotnet run --project HotelSystem
   ```
4. Open `/swagger` to try the API.

---

## 🇪🇬 بالعربي

نظام لإدارة فندق: بيسجّل الغرف ومميزاتها، والحجوزات والعروض والمدفوعات وتقييمات النزلاء. وكمان بيحدد صلاحيات كل موظف، يعني الريسبشن يقدر يحجز، والمدير بس هو اللي يقدر يلغي. المشروع ده هو "العقل" اللي ورا النظام.
