import { Component, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ShopcartComponent } from '../shopcart/shopcart.component';
import { AuthService } from 'src/app/auth/services/auth.service';
import { UserService } from 'src/app/services/user.service';
import { User } from 'src/app/models/user';
import { CategoryService } from 'src/app/services/category.service';
import { Category } from 'src/app/models/category';
import { PlantService } from 'src/app/services/plant.service';
import { EditProfileComponent } from '../edit-profile/edit-profile.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderComponent } from '../order/order.component';
import { AddNewPlantComponent } from '../admin-dialogd/add-new-plant/add-new-plant.component';
import { AddNewCategoryComponent } from '../admin-dialogd/add-new-category/add-new-category.component';
import { identifierModuleUrl } from '@angular/compiler';


@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  search: string = "";
  isUserAuth: boolean = false;
  user!: User;
  categories!: Category[];

  constructor(
    private authService: AuthService,
    private snackBar: MatSnackBar,
    private userService: UserService,
    private categoryService: CategoryService,
    private plantService: PlantService,
    private router: Router,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.userService.getUser().subscribe(result => {
      if (result) {
        this.user = result;
        this.isUserAuth = true;
      }
    },
      error => {

      });
    this.categoryService.getAllCategories().subscribe(result => {
      if (result) {
        this.categories = result;
      }
    },
      error => {

      }
    )
  }

  goToHome() {
    this.plantService.getFavPlants().subscribe(result =>{
      if(result)
      {
        this.plantService.selectedPlants.next(result);
      }
    })
    this.router.navigateByUrl('');
  }

  logOut() {
    this.authService.Logout().subscribe(result => {
      if (result) {
        this.isUserAuth = false;
        this.router.navigateByUrl('');
      }
    },
      error => {

      })
  }

  openShopCart() {

    if (this.isUserAuth) {
      const dialogRef = this.dialog.open(ShopcartComponent, {
        width: '600px',
        height: '530px',
      });
    }
    else {
      this.snackBar.open("You must log in to the site to access", '', {
        duration: 2000,
      })
    }

  }

  openEditProfileDialog() {

    const dialogRef = this.dialog.open(EditProfileComponent, {
      width: '600px',
      height: '530px',
      data: this.user
    });

    dialogRef.afterClosed().subscribe(result => {
      this.user = result;
      this.userService.updateUser(result).subscribe(result => {
        if (result) {
          this.snackBar.open('The data was saved', '', {
            duration: 2000,
          })
        }
        else {
          this.snackBar.open('Data saving error')
        }
      })
    });

  }

  openPlantInCategory(category: Category) {

    this.plantService.selectedCategory.next(category);
    this.router.navigateByUrl('');
  }

  startSearch() {
    this.plantService.search(this.search).subscribe(result => {
      this.plantService.selectedPlants.next(result);
    });

  }

  openAllProduct() {
    this.plantService.getAllPlants().subscribe(result => {
      this.plantService.selectedPlants.next(result);
    });
  }

  openOrders() {
    if (this.isUserAuth) {
      const dialogRef = this.dialog.open(OrderComponent, {
        width: '600px',
        height: '530px',
      });
    }
    else {
      this.snackBar.open("You must log in to the site to access", '', {
        duration: 2000,
      })
    }
  }

  openAddNewPlant() {
    const dialogRef = this.dialog.open(AddNewPlantComponent, {
      width: '600px',
      height: '600px',
    });

  }

  openAddNewCategory() {
    const dialogRef = this.dialog.open(AddNewCategoryComponent, {
      width: '600px',
      height: '400px',
    });

  }
}
