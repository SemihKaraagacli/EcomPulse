export class ProductUpdateViewModel {
  id: number;
  name: string;
  description: string;
  price: number;
  stock: number;
  categoryId: string;

  constructor(
    id: number,
    name: string,
    description: string,
    price: number,
    stock: number,
    categoryId: string
  ) {
    this.id = id;
    this.name = name;
    this.description = description;
    this.price = price;
    this.stock = stock;
    this.categoryId = categoryId;
  }
}
