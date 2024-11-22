import Product from "../models/Product";
import { Card } from 'antd';
import { StarOutlined } from "@ant-design/icons";
import "../ProductCard.css";

interface ProductProps {
    product: Product;
}

export const ProductComponent = ({ product }: ProductProps) => {
    return (
        <Card
            key={product.asin}
            hoverable
            className="product-card"
            cover={
                <img alt={product.title} src={product.imgUrl} className="product-image" />
            }
        >
            <Card.Meta
                title={
                    <a href={product.productUrl} target="_blank" rel="noopener noreferrer">
                        {product.title}
                    </a>
                }
                description={
                    <div className="product-info">
                        <div>
                            <StarOutlined /> {product.stars} ({product.reviews} reviews)
                        </div>
                        <div>
                            <b>${product.price.toFixed(2)}</b>{" "}
                            <del style={{ color: "gray" }}>
                                {product.listPrice > product.price && `$${product.listPrice.toFixed(2)}`}
                            </del>{" "}
                            {product.discount > 0 && (
                                <span style={{ color: "green" }}>-{product.discount}%</span>
                            )}
                        </div>
                        <div>Bought last month: {product.boughtLastMonth}</div>
                    </div>
                }
            />
        </Card>
    );
}
