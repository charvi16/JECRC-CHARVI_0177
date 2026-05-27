// src/app/modules/shipments/shipments-list/shipments-list.component.ts
import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CustomerApiService, DriverApiService, ShipmentApiService, VehicleApiService } from '../../../core/services/api.service';
import { Customer, Driver, PagedResult, Shipment, Vehicle } from '../../../core/models/models';

@Component({
  selector: 'app-shipments-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  template: `
    <div class="shipments-page">
      <div class="page-header">
        <div>
          <h2>Shipments</h2>
          <p>Manage and track all your shipments</p>
        </div>
        <button class="btn-primary" (click)="openNewShipment()">+ New Shipment</button>
      </div>

      <!-- Filters -->
      <div class="filters-bar">
        <div class="search-box">
          <span>🔍</span>
          <input [(ngModel)]="searchTerm" placeholder="Search by tracking number, customer..." />
        </div>
        <div class="filter-chips">
          <button *ngFor="let s of statusFilters"
                  [class.active]="activeStatus() === s.value"
                  (click)="filterByStatus(s.value)"
                  class="chip">{{ s.label }}</button>
        </div>
      </div>

      <!-- Loading -->
      <div *ngIf="loading()" class="loading-state">
        <div class="spinner"></div> Loading shipments...
      </div>

      <!-- Table -->
      <div class="table-card" *ngIf="!loading()">
        <table class="data-table">
          <thead>
            <tr>
              <th>Order ID</th>
              <th>Customer</th>
              <th>Route</th>
              <th>Driver / Vehicle</th>
              <th>Shipping Method</th>
              <th>ETA</th>
              <th>Status</th>
              <th></th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let s of filteredShipments()">
              <td>
                <a [routerLink]="['/shipments', s.id]" class="tracking-link">{{ s.trackingNumber }}</a>
              </td>
              <td>{{ s.customerName || '—' }}</td>
              <td>
                <span class="route-text">{{ s.origin }} → {{ s.destination }}</span>
              </td>
              <td>
                <div *ngIf="s.driverName">{{ s.driverName }}</div>
                <div class="sub-text" *ngIf="s.vehicleName">{{ s.vehicleName }}</div>
                <span *ngIf="!s.driverName" class="unassigned">Unassigned</span>
              </td>
              <td>{{ s.shippingMethod }}</td>
              <td>{{ s.estimatedDelivery ? (s.estimatedDelivery | date:'MMM d') : '—' }}</td>
              <td>
                <span class="status-badge" [class]="getStatusClass(s.status)">{{ s.status }}</span>
              </td>
              <td>
                <button class="action-btn" title="Assign driver and vehicle" (click)="openAssign(s)">Assign</button>
              </td>
            </tr>
            <tr *ngIf="filteredShipments().length === 0">
              <td colspan="8" class="empty-state">No shipments found</td>
            </tr>
          </tbody>
        </table>

        <!-- Pagination -->
        <div class="pagination" *ngIf="result">
          <span class="page-info">Showing {{ shipments.length }} of {{ result.totalCount }}</span>
          <div class="page-controls">
            <button [disabled]="currentPage() === 1" (click)="changePage(currentPage() - 1)">‹</button>
            <button *ngFor="let p of pages" [class.active]="p === currentPage()" (click)="changePage(p)">{{ p }}</button>
            <button [disabled]="currentPage() === result.totalPages" (click)="changePage(currentPage() + 1)">›</button>
          </div>
        </div>
      </div>

      <div class="modal-overlay" *ngIf="creatingShipment()" (click)="creatingShipment.set(false)">
        <form class="modal" (click)="$event.stopPropagation()" (ngSubmit)="createShipment()">
          <h3>New Shipment</h3>
          <div class="form-grid">
            <label>Customer
              <select name="customerId" [(ngModel)]="shipmentForm.customerId" required>
                <option value="">Select customer</option>
                <option *ngFor="let c of customers" [value]="c.id">{{ c.name }}</option>
              </select>
            </label>
            <label>Shipping Method
              <select name="shippingMethod" [(ngModel)]="shipmentForm.shippingMethod">
                <option>Standard</option>
                <option>Fast</option>
                <option>Express</option>
              </select>
            </label>
            <label>Origin<input name="origin" [(ngModel)]="shipmentForm.origin" required /></label>
            <label>Destination<input name="destination" [(ngModel)]="shipmentForm.destination" required /></label>
            <label>Weight (kg)<input name="weight" type="number" [(ngModel)]="shipmentForm.weight" /></label>
            <label>ETA<input name="eta" type="date" [(ngModel)]="shipmentForm.estimatedDelivery" /></label>
          </div>
          <div class="modal-actions">
            <button type="button" class="btn-secondary" (click)="creatingShipment.set(false)">Cancel</button>
            <button type="submit" class="btn-primary" [disabled]="savingShipment()">{{ savingShipment() ? 'Saving...' : 'Create' }}</button>
          </div>
        </form>
      </div>

      <div class="modal-overlay" *ngIf="assigningShipment()" (click)="assigningShipment.set(null)">
        <form class="modal small" (click)="$event.stopPropagation()" (ngSubmit)="assignShipment()">
          <h3>Assign Delivery</h3>
          <p class="modal-sub">{{ assigningShipment()?.trackingNumber }}</p>
          <label>Driver
            <select name="driverId" [(ngModel)]="assignForm.driverId" required>
              <option value="">Select driver</option>
              <option *ngFor="let d of drivers" [value]="d.id">{{ d.fullName || (d.firstName + ' ' + d.lastName) }} - {{ d.status }}</option>
            </select>
          </label>
          <label>Vehicle
            <select name="vehicleId" [(ngModel)]="assignForm.vehicleId" required>
              <option value="">Select vehicle</option>
              <option *ngFor="let v of vehicles" [value]="v.id">{{ v.name }} - {{ v.licensePlate }}</option>
            </select>
          </label>
          <div class="modal-actions">
            <button type="button" class="btn-secondary" (click)="assigningShipment.set(null)">Cancel</button>
            <button type="submit" class="btn-primary" [disabled]="savingAssignment()">{{ savingAssignment() ? 'Assigning...' : 'Assign' }}</button>
          </div>
        </form>
      </div>
    </div>
  `,
  styles: [`
    .shipments-page { display: flex; flex-direction: column; gap: 20px; }
    .page-header { display: flex; justify-content: space-between; align-items: flex-start; }
    .page-header h2 { font-size: 22px; font-weight: 700; margin: 0 0 4px; }
    .page-header p { color: #64748b; margin: 0; font-size: 14px; }
    .btn-primary { background: #1d4ed8; color: white; border: none; padding: 10px 20px; border-radius: 8px; font-weight: 600; cursor: pointer; }

    .filters-bar { display: flex; gap: 16px; align-items: center; }
    .search-box { display: flex; align-items: center; gap: 8px; background: white; border: 1px solid #e2e8f0; border-radius: 8px; padding: 8px 12px; flex: 1; max-width: 400px; }
    .search-box input { border: none; outline: none; flex: 1; font-size: 14px; }
    .filter-chips { display: flex; gap: 8px; }
    .chip { padding: 6px 14px; border: 1px solid #e2e8f0; border-radius: 20px; background: white; cursor: pointer; font-size: 13px; color: #64748b; }
    .chip.active { background: #1d4ed8; color: white; border-color: #1d4ed8; }

    .loading-state { display: flex; align-items: center; gap: 12px; padding: 40px; justify-content: center; color: #64748b; }
    .spinner { width: 20px; height: 20px; border: 2px solid #e2e8f0; border-top-color: #3b82f6; border-radius: 50%; animation: spin 0.8s linear infinite; }
    @keyframes spin { to { transform: rotate(360deg); } }

    .table-card { background: white; border-radius: 12px; box-shadow: 0 1px 4px rgba(0,0,0,.06); overflow: hidden; }
    .data-table { width: 100%; border-collapse: collapse; font-size: 14px; }
    .data-table th { text-align: left; padding: 12px 16px; background: #f8fafc; color: #64748b; font-weight: 500; font-size: 12px; text-transform: uppercase; letter-spacing: 0.5px; border-bottom: 1px solid #f1f5f9; }
    .data-table td { padding: 14px 16px; border-bottom: 1px solid #f8fafc; color: #374151; vertical-align: middle; }
    .data-table tr:hover td { background: #fafafa; }

    .tracking-link { color: #1d4ed8; text-decoration: none; font-weight: 600; }
    .route-text { color: #374151; }
    .sub-text { font-size: 12px; color: #94a3b8; margin-top: 2px; }
    .unassigned { color: #94a3b8; font-style: italic; }
    .empty-state { text-align: center; color: #94a3b8; padding: 40px !important; }
    .action-btn { background: #eff6ff; border: 1px solid #bfdbfe; cursor: pointer; font-size: 12px; color: #1d4ed8; border-radius: 6px; padding: 5px 9px; font-weight: 600; }

    .status-badge { padding: 4px 10px; border-radius: 20px; font-size: 12px; font-weight: 500; }
    .status-intransit { background: #dbeafe; color: #1d4ed8; }
    .status-delivered { background: #dcfce7; color: #16a34a; }
    .status-pending { background: #f1f5f9; color: #64748b; }
    .status-delayed { background: #fef2f2; color: #dc2626; }
    .status-pickupready { background: #fefce8; color: #ca8a04; }
    .status-cancelled { background: #fef2f2; color: #dc2626; }

    .pagination { display: flex; justify-content: space-between; align-items: center; padding: 16px 20px; border-top: 1px solid #f1f5f9; }
    .page-info { font-size: 13px; color: #64748b; }
    .page-controls { display: flex; gap: 4px; }
    .page-controls button { width: 32px; height: 32px; border: 1px solid #e2e8f0; background: white; border-radius: 6px; cursor: pointer; font-size: 14px; }
    .page-controls button.active { background: #1d4ed8; color: white; border-color: #1d4ed8; }
    .page-controls button:disabled { opacity: 0.4; cursor: not-allowed; }

    .modal-overlay { position: fixed; inset: 0; background: rgba(15,23,42,.45); display: flex; align-items: center; justify-content: center; z-index: 1000; }
    .modal { width: 620px; background: white; border-radius: 12px; padding: 22px; box-shadow: 0 24px 70px rgba(15,23,42,.25); display: flex; flex-direction: column; gap: 14px; }
    .modal.small { width: 420px; }
    .modal h3 { margin: 0; font-size: 18px; }
    .modal-sub { margin: -8px 0 4px; color: #64748b; font-size: 13px; }
    .form-grid { display: grid; grid-template-columns: repeat(2, 1fr); gap: 12px; }
    .modal label { display: flex; flex-direction: column; gap: 6px; font-size: 12px; color: #475569; font-weight: 600; }
    .modal input, .modal select { border: 1px solid #cbd5e1; border-radius: 8px; padding: 9px 10px; font-size: 14px; }
    .modal-actions { display: flex; justify-content: flex-end; gap: 10px; margin-top: 4px; }
    .btn-secondary { background: white; color: #334155; border: 1px solid #cbd5e1; padding: 10px 16px; border-radius: 8px; font-weight: 600; cursor: pointer; }
  `]
})
export class ShipmentsListComponent implements OnInit {
  private api = inject(ShipmentApiService);
  private customersApi = inject(CustomerApiService);
  private driversApi = inject(DriverApiService);
  private vehiclesApi = inject(VehicleApiService);

  shipments: Shipment[] = [];
  customers: Customer[] = [];
  drivers: Driver[] = [];
  vehicles: Vehicle[] = [];
  result: PagedResult<Shipment> | null = null;
  loading = signal(true);
  currentPage = signal(1);
  activeStatus = signal<string>('');
  creatingShipment = signal(false);
  savingShipment = signal(false);
  assigningShipment = signal<Shipment | null>(null);
  savingAssignment = signal(false);
  searchTerm = '';
  shipmentForm = this.emptyShipmentForm();
  assignForm = { driverId: '', vehicleId: '' };

  statusFilters = [
    { label: 'All', value: '' },
    { label: 'In Transit', value: 'InTransit' },
    { label: 'Pickup Ready', value: 'PickupReady' },
    { label: 'Delivered', value: 'Delivered' },
    { label: 'Delayed', value: 'Failed' },
  ];

  get pages(): number[] {
    if (!this.result) return [];
    return Array.from({ length: Math.min(this.result.totalPages, 5) }, (_, i) => i + 1);
  }

  ngOnInit() {
    this.load();
    this.customersApi.getAll().subscribe(cs => this.customers = cs);
    this.driversApi.getAll().subscribe(ds => this.drivers = ds);
    this.vehiclesApi.getAll().subscribe(vs => this.vehicles = vs);
  }

  filteredShipments(): Shipment[] {
    const q = this.searchTerm.trim().toLowerCase();
    if (!q) return this.shipments;
    return this.shipments.filter(s =>
      s.trackingNumber.toLowerCase().includes(q) ||
      (s.customerName ?? '').toLowerCase().includes(q) ||
      s.origin.toLowerCase().includes(q) ||
      s.destination.toLowerCase().includes(q) ||
      (s.driverName ?? '').toLowerCase().includes(q) ||
      (s.vehicleName ?? '').toLowerCase().includes(q)
    );
  }

  load() {
    this.loading.set(true);
    this.api.getAll(this.currentPage(), 20, this.activeStatus() || undefined).subscribe({
      next: result => {
        this.result = result;
        this.shipments = result.items;
        this.loading.set(false);
      },
      error: () => {
        // Demo data when backend isn't available
        this.shipments = this.getDemoShipments();
        this.loading.set(false);
      }
    });
  }

  filterByStatus(status: string) {
    this.activeStatus.set(status);
    this.currentPage.set(1);
    this.load();
  }

  changePage(page: number) {
    this.currentPage.set(page);
    this.load();
  }

  openNewShipment() {
    this.shipmentForm = this.emptyShipmentForm();
    this.creatingShipment.set(true);
  }

  createShipment() {
    if (!this.shipmentForm.customerId || !this.shipmentForm.origin || !this.shipmentForm.destination) return;
    this.savingShipment.set(true);
    this.api.create({
      ...this.shipmentForm,
      estimatedDelivery: this.shipmentForm.estimatedDelivery || undefined,
      items: []
    }).subscribe({
      next: () => {
        this.savingShipment.set(false);
        this.creatingShipment.set(false);
        this.load();
      },
      error: () => this.savingShipment.set(false)
    });
  }

  openAssign(shipment: Shipment) {
    this.assigningShipment.set(shipment);
    this.assignForm = { driverId: '', vehicleId: '' };
  }

  assignShipment() {
    const shipment = this.assigningShipment();
    if (!shipment || !this.assignForm.driverId || !this.assignForm.vehicleId) return;
    this.savingAssignment.set(true);
    this.api.assign(shipment.id, this.assignForm.driverId, this.assignForm.vehicleId).subscribe({
      next: () => {
        this.savingAssignment.set(false);
        this.assigningShipment.set(null);
        this.load();
      },
      error: () => this.savingAssignment.set(false)
    });
  }

  getStatusClass(status: string): string {
    const map: Record<string, string> = {
      InTransit: 'status-intransit',
      Delivered: 'status-delivered',
      Pending: 'status-pending',
      Failed: 'status-delayed',
      PickupReady: 'status-pickupready',
      Cancelled: 'status-cancelled',
    };
    return map[status] ?? 'status-pending';
  }

  private getDemoShipments(): Shipment[] {
    return [
      { id: '1', trackingNumber: '#19949062930768', origin: 'Delhi', destination: 'Chennai', status: 'InTransit', shippingMethod: 'Standard', weight: 120, customerName: 'Compass East Corp.', driverName: 'Jimmy Dente', vehicleName: 'PeterBit 500', estimatedDelivery: '2024-07-26', createdAt: new Date().toISOString(), trackingEvents: [] },
      { id: '2', trackingNumber: '#03234614845123', origin: 'Gurgaon', destination: 'Bangalore', status: 'Delivered', shippingMethod: 'Standard', weight: 80, customerName: 'Cyberdyne Industries', driverName: 'Maria Santos', vehicleName: 'Box Truck', estimatedDelivery: '2024-07-26', createdAt: new Date().toISOString(), trackingEvents: [] },
      { id: '3', trackingNumber: '#19949065628573', origin: 'Mumbai', destination: 'Pune', status: 'PickupReady', shippingMethod: 'Express', weight: 45, customerName: 'DHL Logistics', driverName: null!, vehicleName: null!, estimatedDelivery: '2024-07-27', createdAt: new Date().toISOString(), trackingEvents: [] },
      { id: '4', trackingNumber: '#19949068727406', origin: 'Hyderabad', destination: 'Chennai', status: 'Pending', shippingMethod: 'Fast', weight: 200, customerName: 'Amazon India', driverName: null!, vehicleName: null!, estimatedDelivery: '2024-07-28', createdAt: new Date().toISOString(), trackingEvents: [] },
    ];
  }

  private emptyShipmentForm() {
    return {
      origin: '',
      destination: '',
      estimatedDelivery: '',
      shippingMethod: 'Standard',
      weight: 1,
      customerId: ''
    };
  }
}
