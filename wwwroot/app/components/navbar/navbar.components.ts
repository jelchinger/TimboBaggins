import { Component } from '@angular/core';
import { AuthService } from "../../auth.service";
import { Router } from '@angular/router';
import { Formbuilder, Validators } from '@angular/forms';
@Component({
    selector: 'nav-bar',
    templateUrl: './app/components/navbar/navbar.component.html',
    styleUrls: ['./app/components/navbar/navbar.component.css']
})
export class NavBarComponent {
constructor(public router: Router, public authService: AuthService) { }
 isActive(data: any[]): boolean {
 return this.router.isActive(
 this.router.createUrlTree(data),
 true);
 }
 logout(): boolean {
 // logs out the user, then redirects him to Welcome View.
 if (this.authService.logout()) {
 this.router.navigate([""]);
 }
 return false;
 }
}
