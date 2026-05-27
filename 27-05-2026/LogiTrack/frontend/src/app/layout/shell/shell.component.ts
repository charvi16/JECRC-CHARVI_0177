// src/app/layout/shell/shell.component.ts
import { Component, inject, signal } from '@angular/core';
import { Router, RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { CommonModule } from '@angular/common';

interface NavItem {
  label: string;
  icon: string;
  route: string;
  badge?: number;
}

@Component({
  selector: 'app-shell',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive, CommonModule],
  template: `
    <div class="shell">
      <!-- Sidebar -->
      <aside class="sidebar" [class.collapsed]="sidebarCollapsed()">
        <div class="logo">
          <span class="logo-icon">🚚</span>
          <span class="logo-text" *ngIf="!sidebarCollapsed()">LogiTrack</span>
        </div>

        <nav class="nav">
          <a *ngFor="let item of navItems"
             [routerLink]="item.route"
             routerLinkActive="active"
             class="nav-item"
             [title]="item.label">
            <span class="nav-icon">{{ item.icon }}</span>
            <span class="nav-label" *ngIf="!sidebarCollapsed()">{{ item.label }}</span>
            <span class="badge" *ngIf="item.badge">{{ item.badge }}</span>
          </a>
        </nav>

        <button class="collapse-btn" (click)="sidebarCollapsed.set(!sidebarCollapsed())">
          {{ sidebarCollapsed() ? '›' : '‹' }}
        </button>
      </aside>

      <!-- Main Content -->
      <div class="main">
        <header class="topbar">
          <div class="topbar-left">
            <h1 class="page-title">LogiTrack</h1>
          </div>
          <div class="topbar-right">
            <div class="search-box">
              <input type="text" placeholder="Search shipments, vehicles..." />
            </div>
            <div class="notif-btn">🔔</div>
            <div class="user-menu">
              <div class="user-avatar">{{ initials }}</div>
              <button class="logout-btn" (click)="logout()">Logout</button>
            </div>
          </div>
        </header>

        <main class="content">
          <router-outlet />
        </main>
      </div>
    </div>
  `,
  styles: [`
    .shell { display: flex; height: 100vh; background: #eef2f7; font-family: 'Inter', sans-serif; }

    .sidebar {
      width: 240px; background: #111827; display: flex; flex-direction: column;
      transition: width 0.3s; overflow: hidden; flex-shrink: 0;
    }
    .sidebar.collapsed { width: 64px; }

    .logo {
      display: flex; align-items: center; gap: 12px;
      padding: 20px 16px; border-bottom: 1px solid #1e293b;
    }
    .logo-icon { font-size: 24px; }
    .logo-text { color: #38bdf8; font-weight: 700; font-size: 18px; white-space: nowrap; }

    .nav { flex: 1; padding: 12px 8px; display: flex; flex-direction: column; gap: 4px; }

    .nav-item {
      display: flex; align-items: center; gap: 12px;
      padding: 10px 12px; border-radius: 8px; text-decoration: none;
      color: #94a3b8; transition: all 0.2s; position: relative; white-space: nowrap;
    }
    .nav-item:hover { background: #1f2937; color: #e5e7eb; }
    .nav-item.active { background: #0f766e; color: #fff; }
    .nav-icon { font-size: 18px; min-width: 20px; text-align: center; }
    .nav-label { font-size: 14px; font-weight: 500; }
    .badge {
      margin-left: auto; background: #ef4444; color: white;
      font-size: 10px; padding: 2px 6px; border-radius: 10px;
    }

    .collapse-btn {
      margin: 12px; padding: 8px; background: #1e293b; border: none;
      color: #94a3b8; border-radius: 8px; cursor: pointer; font-size: 18px;
    }

    .main { flex: 1; display: flex; flex-direction: column; overflow: hidden; }

    .topbar {
      background: white; padding: 0 24px; height: 64px;
      display: flex; align-items: center; justify-content: space-between;
      border-bottom: 1px solid #e2e8f0; box-shadow: 0 1px 4px rgba(0,0,0,.05);
    }
    .page-title { font-size: 18px; font-weight: 700; color: #0f172a; }
    .topbar-right { display: flex; align-items: center; gap: 16px; }
    .search-box input {
      border: 1px solid #e2e8f0; border-radius: 8px; padding: 8px 16px;
      width: 300px; font-size: 14px; outline: none;
    }
    .search-box input:focus { border-color: #3b82f6; }
    .notif-btn { font-size: 20px; cursor: pointer; }
    .user-avatar {
      width: 36px; height: 36px; border-radius: 50%;
      background: #0f766e; color: white; display: flex;
      align-items: center; justify-content: center; font-weight: 600; font-size: 13px;
    }
    .user-menu { display: flex; align-items: center; gap: 8px; }
    .logout-btn { border: 1px solid #cbd5e1; background: white; color: #334155; border-radius: 8px; padding: 7px 10px; cursor: pointer; font-size: 12px; font-weight: 600; }

    .content { flex: 1; overflow-y: auto; padding: 24px; }
  `]
})
export class ShellComponent {
  private router = inject(Router);
  sidebarCollapsed = signal(false);
  private user = JSON.parse(localStorage.getItem('user') || '{"name":"System Team"}');
  initials = String(this.user.name || this.user.email || 'ST')
    .split(' ')
    .map(part => part[0])
    .join('')
    .slice(0, 2)
    .toUpperCase();

  navItems: NavItem[] = [
    { label: 'Dashboard', icon: '📊', route: '/dashboard' },
    { label: 'Shipments', icon: '📦', route: '/shipments', badge: 12 },
    { label: 'Fleet', icon: '🚛', route: '/fleet' },
    { label: 'Warehouse', icon: '🏭', route: '/warehouse' },
    { label: 'Tracking', icon: '📍', route: '/tracking' },
    { label: 'Routes', icon: '🗺️', route: '/routes' },
    { label: 'Analytics', icon: '📈', route: '/analytics' },
    { label: 'Customers', icon: '👥', route: '/customers' },
  ];

  logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
