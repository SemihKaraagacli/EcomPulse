export class ProductCreateRequestViewModel {
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;

  constructor(
    name: string,
    description: string,
    price: number,
    stock: number,
    categoryId: string
  ) {
    this.name = name;
    this.description = description;
    this.price = price;
    this.stock = stock;
    this.categoryId = categoryId;
  }
}
