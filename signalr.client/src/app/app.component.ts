import { Component, ViewChild } from '@angular/core';
import { GridComponent, EditEventArgs, ToolbarItems, SearchService } from '@syncfusion/ej2-angular-grids';
import { DataManager, UrlAdaptor } from '@syncfusion/ej2-data';
import { EditSettingsModel } from '@syncfusion/ej2-angular-grids';
import { HubConnection} from '@microsoft/signalr';
import * as signalR from '@microsoft/signalr';

@Component({ 
  selector: 'app-root',
  templateUrl: 'app.component.html',
  styleUrl:'app.component.css'
})
export class AppComponent {
  @ViewChild('grid')
  public grid?: GridComponent;
  public inventoryData?: DataManager;
  public editSettings?: EditSettingsModel;
  public toolbar?: ToolbarItems[];
  public ItemIdRules?: object;
  public customerIDRules?: object;
  public ItemNameRules?: object;
  public CategoryRules?: object;
  public QuantityRules?: object;
  public UnitPriceRules?: object;
  public LocationCodeRules?: object;
  public SupplierNameRules?: object;
  public ReorderLevelRules?: object;
  public flag = false;
  private connection!: HubConnection;
  public pageSettings = { pageSize: 10 };

  ngOnInit(): void {
    this.inventoryData = new DataManager({
      url: 'http://localhost:5083/Home/UrlDatasource',
      updateUrl: 'http://localhost:5083/Home/Update',
      insertUrl: 'http://localhost:5083/Home/Insert',
      removeUrl: 'http://localhost:5083/Home/Delete',
      adaptor: new UrlAdaptor()
    }); //Use remote server host number instead 5083
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl("http://localhost:5083/ChatHub")  //Use remote server host instead number 5083
      .build();
    this.editSettings = { allowEditing: true, allowAdding: true, allowDeleting: true, mode: 'Normal' };
    this.toolbar = ['Add', 'Edit', 'Delete', 'Update', 'Cancel', 'Search'];
    this.ItemIdRules = { required: true };
    this.customerIDRules = { required: true, minLength: 3 };
    this.ItemNameRules = { required: true, minLength: 3 };
    this.CategoryRules = { required: true };
    this.QuantityRules = { required: true, number: true, min: 0 };
    this.UnitPriceRules = { required: true, min: 0 };
    this.LocationCodeRules = { maxLength: 10 };
    this.SupplierNameRules = { required: true, minLength: 3 };
    this.ReorderLevelRules = { required: true, number: true, min: 0 };
  }
  created() {
    // Adds to the connection object a handler that receives messages from the hub
    this.connection.on("ReceiveMessage", function (message: string) {
      var grid = (document.getElementsByClassName('e-grid')[0] as HTMLFormElement)["ej2_instances"][0];
      grid.refresh();
    });

    // Starts a connection.
    this.connection.start()
  .then(() => {
    console.log("SignalR connection established successfully");
    // Now that the connection is established
    this.connection.invoke('SendMessage', "refreshPages")
      .catch((err: Error) => {
        console.error("Error sending data:", err.toString());
      });
  })
  .catch((err: Error) => {
    console.error("Error establishing SignalR connection:", err.toString());
  });

  }
  actionComplete(args: EditEventArgs) {
    if (args.requestType == "save" || args.requestType == "delete") {
      //send a message from a connected client to all clients.
      this.connection.invoke('SendMessage', "refreshPages")
        .catch((err:Error ) => {
          console.error(err.toString());
        });
    }
  }

  
 // ======= UI Helpers used in templates =======
 
currencyCode = 'INR'; // or bind based on user/company settings

supplierInitials(name?: string): string {
  if (!name) return '—';
  const parts = name.trim().split(/\s+/);
  const initials = (parts[0]?.[0] || '') + (parts[1]?.[0] || '');
  return initials.toUpperCase();
}

}
