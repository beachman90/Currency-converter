# Currency Converter

A full-stack currency conversion application with historical rate tracking.

## Project Structure

Currency-converter/
├── frontend/           # Web interface
│   ├── src/           # Source files
│   │   └── input.css  # Tailwind CSS source
│   ├── css/           # Generated CSS
│   ├── index.html     # Main application
│   ├── package.json   # Frontend dependencies
│   ├── postcss.config.js # PostCSS configuration
│   └── tailwind.config.js # Tailwind configuration
└── backend/           # .NET Web API
    └── Data/          # Database context and models

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Node.js](https://nodejs.org/) (v14 or higher)
- [npm](https://www.npmjs.com/) (comes with Node.js)
- [SQL Server LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

## Quick Start

### Backend Setup

1. Navigate to the backend directory:

cd backend

2. Restore dependencies:

dotnet restore

3. Ensure SQL Server LocalDB is installed and running:

sqllocaldb start MSSQLLocalDB

4. Start the API:

dotnet run

The API will be available at `https://localhost:7258`

### Frontend Setup

1. Navigate to the frontend directory:

cd frontend

2. Install dependencies:

npm install

3. Create the css directory if it doesn't exist:

mkdir css

4. Start the Tailwind CSS build process:

npm run dev

5. Open `index.html` in your browser using a local server (important for CORS):
   - Using Visual Studio Code: Install "Live Server" extension and click "Go Live"
   - Using Python: `python -m http.server 8080`
   - Using Node.js: `npx serve`

## Required NPM Packages

The frontend requires these packages (they will be installed with `npm install`):

```json
{
  "dependencies": {
    "autoprefixer": "^10.4.17",
    "postcss": "^8.4.35",
    "postcss-cli": "^8.3.1",
    "tailwindcss": "^3.4.1"
  }
}
```

## Configuration Files

### postcss.config.js

```javascript
module.exports = {
  plugins: {
    tailwindcss: {},
    autoprefixer: {},
  },
}
```

### tailwind.config.js

```javascript
/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./*.{html,js}"],
  darkMode: "class",
  theme: {
    extend: {},
  },
  plugins: [],
}
```

## API Endpoints

### Currency Conversion
- `POST /api/Currency/convert` - Convert between currencies
  - Body: `{ fromCurrency: string, toCurrency: string, amount: number, date?: string }`

### Historical Rates
- `GET /api/RatesOverTime/rates` - Get historical rates for a currency
  - Query params: 
    - `currency`: Currency code (e.g., "USD")
    - `timeStart`: Start date
    - `timeEnd`: End date

## Database Configuration

The application uses SQL Server LocalDB with the following connection string:

```
Server=(localdb)\MSSQLLocalDB;Database=CurrencyDb;Trusted_Connection=True;
```

## Common Issues and Solutions

1. CORS Errors:
   - Make sure to use a local server to serve the frontend
   - Don't open index.html directly in the browser

2. Database Connection:
   - Ensure LocalDB is running
   - Check if CurrencyDb database exists
   - Verify Windows Authentication is working

3. CSS Not Loading:
   - Ensure the css directory exists
   - Check if Tailwind build process is running
   - Verify css/style.css is being generated

4. API Connection:
   - Verify the backend is running on port 7258
   - Check for any SSL certificate issues
   - Ensure all HTTPS requests are allowed

## Development

### Backend Development
- Built with .NET 8.0
- Uses Entity Framework Core for database operations
- SQL Server LocalDB for storing exchange rates
- RESTful API design

### Frontend Development
- HTML5 with Tailwind CSS
- Vanilla JavaScript
- Chart.js for data visualization
- Dark/Light theme support

## Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

This project is licensed under the MIT License - see the LICENSE file for details
