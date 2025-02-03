import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '', 
        redirectTo: 'users',
        pathMatch: 'full'
      },
      {
        path: 'users',
        loadComponent: () => import('./features/users/views/users-list/users-list.component').then(c => c.UsersListComponent)
      },
      {
        path: 'create-user',
        loadComponent: () => import('./features/users/views/user-detail/user-detail.component').then(c => c.UserDetailComponent)
      },
      {
        path: 'users/:id',
        loadComponent: () => import('./features/users/views/user-detail/user-detail.component').then(c => c.UserDetailComponent)
      },
];
