# Syncfusion Angular Grid with SignalR

The Syncfusion Angular Grid component supports real-time data binding using SignalR, enabling automatic grid updates as data changes on the server. This capability proves essential for applications requiring live updates and multi-client synchronization.

## Key Features

- **Real-time updates:** Automatically refreshes Grid data when server-side changes occur.
- **Two‑way communication:** Enables instant data exchange between client and server.
- **Live CRUD sync:** Add, edit, and delete operations reflect immediately across all clients.
- **Automatic reconnection:** Handles connection drops and restores updates seamlessly.
- **Optimized transport:** Uses WebSockets or fallback transports for fast, reliable updates.

## Prerequisites

| Software / Package          | Recommended version          | Purpose                                 |
|-----------------------------|------------------------------|--------------------------------------   |
| Node.js                     | 20.x LTS or later            | Backend runtime                            |
| npm                         | Latest (11.x+)               | Package manager                         | 
| Angular CLI                 | 18.x or later                | Create and manage Angular apps |



## Quick Start

1. **Clone the repository**
   ```bash
   git clone <repository-url>
   ```

2. **Running the application**

**Run the Server:**

- Run the below commands to run the server.
 
  ```bash
    cd SignalR.Server
    dotnet run
  ```
- The server is now running at http://localhost:5083/.

**Run the client**
 
 - Execute the below commands to run the client application.
  
    ```bash
    cd signalr.client
    npm start
    ```
- Open http://localhost:4200/ in the browser.


## Project Layout

| File/Folder | Purpose |
|-------------|---------|
| `signalr.client/src/app/app.component.ts` | Root Angular component logic |
| `signalr.client/src/app/app.component.html` | Root Angular component template |
| `signalr.client/src/app/app.component.css` | Styles for the root component |
| `signalr.client/src/styles.css` | Global styles for the Angular application |
| `Controllers/HomeController.cs` | Basic controller for simple route handling or test responses. |
| `Hubs/ChatHub.cs` | SignalR hub used to broadcast updates to connected clients. |
| `Models/OrdersDetails.cs` | Data model representing order information shared with clients. |
| `appsettings.json` | Main configuration file for server settings. |
| `appsettings.Development.json` | Development-specific configuration overrides. |
| `Program.cs` | Entry point configuring services, middleware, and SignalR routes. |
| `SignalR.Server.csproj` | Project file defining server dependencies and build configuration. |
| `SignalR.Server.http` | VS Code REST client file for testing server endpoints. |


## Common Tasks

### Add a Record
1. Click **Add** in the grid toolbar
2. Fill out fields (productName, productId, category, rating, etc.)
3. Click **Save** to create the record

### Edit a Record
1. Select a row → **Edit**
2. Modify fields → **Update**

### Delete a Record
1. Select a row → **Delete**
2. Confirm deletion

### Search / Filter / Sort
- Use the **Search** box (toolbar) to match across configured columns
- Use column filter icons for equals/contains/date filters
- Click column headers to sort ascending/descending
