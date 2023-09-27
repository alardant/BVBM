import { Package } from "../Enum/packageEnum";

export class Review {
  id!: number;
  description!: string;
  name!: string;
  createdDate!: Date;
  package!: Package;
  userId!: string;
}
