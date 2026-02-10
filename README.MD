# ProduceFlow 🚀

ProduceFlow adalah sistem manajemen aset dan pengadaan barang (procurement) perusahaan yang dirancang modern, skalabel, dan efisien. Proyek ini dibangun menggunakan **Clean Architecture** pada sisi Backend untuk memastikan pemisahan tanggung jawab (Separation of Concerns) yang jelas, serta Frontend yang responsif menggunakan React dan Vite.

Sistem ini memungkinkan perusahaan untuk mengelola inventaris aset, mengajukan permintaan pembelian (purchase requests), mengelola vendor, serta melacak status persetujuan (approval workflows).

---

## 🌟 Fitur Utama

### 🔐 Otentikasi & Keamanan
* **Registrasi & Login Pengguna:** Sistem otentikasi aman menggunakan JWT (JSON Web Token).
* **Role-Based Access:** Manajemen hak akses berbasis peran (UserRole).
* **Password Hashing:** Keamanan kata sandi menggunakan hashing yang kuat.

### 📦 Manajemen Aset
* **CRUD Aset:** Tambah, baca, perbarui, dan hapus data aset perusahaan.
* **Pelacakan Status:** Memantau status aset (misal: tersedia, digunakan, rusak).
* **Kategorisasi:** Pengelompokan aset berdasarkan kategori, departemen, dan lokasi.

### 🛒 Pengadaan (Procurement)
* **Purchase Requests (PR):** Pembuatan dan pengelolaan permintaan pembelian barang.
* **Item Management:** Rincian item dalam setiap permintaan pembelian.
* **Approval Workflow:** Sistem pencatatan log persetujuan (Approval Logs) untuk memvalidasi permintaan.

### 🗂️ Manajemen Master Data
* **Vendor:** Pengelolaan data pemasok/vendor.
* **Lokasi & Departemen:** Manajemen struktur organisasi dan lokasi fisik aset.
* **Kategori:** Pengaturan kategori aset dinamis.

---

## 🛠️ Teknologi yang Digunakan

### Backend (Server-Side)
* **Framework:** .NET 8 (ASP.NET Core Web API)
* **Arsitektur:** Clean Architecture (Domain, Application, Infrastructure, API)
* **Database:** SQL Server
* **ORM:** Entity Framework Core (Code-First Approach)
* **Pattern:** CQRS (Command Query Responsibility Segregation) menggunakan **MediatR**.
* **Validasi:** FluentValidation.
* **Testing:** xUnit untuk Unit Testing.

### Frontend (Client-Side)
* **Framework:** React
* **Build Tool:** Vite
* **Bahasa:** TypeScript
* **Styling:** Tailwind CSS
* **UI Components:** Shadcn UI (berbasis Radix UI)
* **State Management/Hooks:** Custom Hooks (seperti `useAssets`, `useDebounce`).
* **HTTP Client:** Axios

---

## 📂 Susunan Project

Struktur folder mengikuti pola Clean Architecture:

```text
ProduceFlow/
├── client/                     # Frontend Application (React + Vite)
│   ├── public/
│   └── src/
│       ├── components/         # Komponen UI (Shadcn, Fragments)
│       ├── hooks/              # Custom React Hooks
│       ├── lib/                # Utilities & Axios setup
│       ├── pages/              # Halaman Aplikasi
│       ├── services/           # API integration services
│       └── types/              # TypeScript definitions
│
├── src/                        # Backend Source Code
│   ├── ProduceFlow.API/        # Entry point, Controllers
│   ├── ProduceFlow.Application/# Business Logic, CQRS (Commands/Queries), DTOs
│   ├── ProduceFlow.Domain/     # Enterprise logic, Entities, Interfaces
│   └── ProduceFlow.Infrastructure/ # Database Implementation, Repositories, Migrations
│
└── tests/                      # Unit Tests Project
```