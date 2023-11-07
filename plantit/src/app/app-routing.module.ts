import { NgModule } from '@angular/core';
import { PreloadAllModules, RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadChildren: () => import('./pages/home/home.module').then( m => m.HomePageModule)
  },
  {
    path: 'login',
    loadChildren: () => import('./pages/login/login.module').then( m => m.LoginPageModule)
  },
  {
    path: 'trees',
    loadChildren: () => import('./pages/trees/trees.module').then( m => m.TreesPageModule)
  },
  {
    path: 'zones',
    loadChildren: () => import('./pages/zones/zones.module').then( m => m.ZonesPageModule)
  },
  {
    path: 'scan',
    loadChildren: () => import('./pages/scan/scan.module').then( m => m.ScanPageModule)
  },
  {
    path: 'trees/:id',
    loadChildren: () => import('./pages/tree-detail/tree-detail.module').then( m => m.TreeDetailPageModule)
  },
  {
    path: 'register',
    loadChildren: () => import('./pages/register/register.module').then( m => m.RegisterPageModule)
  },
  {
    path: 'passwd-reset',
    loadChildren: () => import('./pages/passwd-reset/passwd-reset.module').then( m => m.PasswdResetPageModule)
  },
  {
    path: 'splashscreen',
    loadChildren: () => import('./pages/splashscreen/splashscreen.module').then( m => m.SplashscreenPageModule)
  },
  {
    path: 'tasks',
    loadChildren: () => import('./pages/tasks/tasks.module').then( m => m.TasksPageModule)
  },
  {
    path: 'tasks/:id',
    loadChildren: () => import('./pages/task-details/task-details.module').then( m => m.TaskDetailsPageModule)
  },
  {
    path: 'zones/:id',
    loadChildren: () => import('./pages/zone-detail/zone-detail.module').then( m => m.ZoneDetailPageModule)
  },
  {
    path: 'add-new-task',
    loadChildren: () => import('./pages/add-new-task/add-new-task.module').then( m => m.AddNewTaskPageModule)
  },
  {
    path: 'update-task',
    loadChildren: () => import('./pages/update-task/update-task.module').then( m => m.UpdateTaskPageModule)
  }


];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })
  ],
  exports: [RouterModule]
})
export class AppRoutingModule {}
