import Product from "../models/Product";
import { Form, Input, InputNumber, Button } from 'antd';

interface ProductFormComponentProps {
    onSubmit: (product: Product) => void;
}

export const ProductFormComponent = ({ onSubmit }: ProductFormComponentProps) => {
    const [form] = Form.useForm();

    const handleFinish = (values: Product) => {
        const resultProduct = new Product();
        resultProduct.title = values.title;
        resultProduct.stars = values.stars;
        resultProduct.reviews = values.reviews;
        resultProduct.price = values.price;
        resultProduct.listPrice = values.listPrice;
        resultProduct.discount = values.listPrice - values.price;
        resultProduct.logPrice = Math.log(values.price);
        resultProduct.logReviews = Math.log(values.reviews);

        onSubmit(resultProduct); 
        form.resetFields(); 
    };

    return (
        <Form
            form={form}
            layout="vertical"
            onFinish={handleFinish}
            style={{ maxWidth: 400, margin: '0 auto' }}
        >
            <Form.Item
                label="Title"
                name="title"
                rules={[{ required: true, message: 'Please enter the product title' }]}
            >
                <Input placeholder="Enter product title" />
            </Form.Item>

            <Form.Item
                label="Stars"
                name="stars"
                rules={[{ required: true, message: 'Please enter the star rating' }]}
            >
                <InputNumber
                    min={0}
                    max={5}
                    step={0.1}
                    placeholder="Enter star rating (0-5)"
                    style={{ width: '100%' }}
                />
            </Form.Item>

            <Form.Item
                label="Reviews"
                name="reviews"
                rules={[{ required: true, message: 'Please enter the number of reviews' }]}
            >
                <InputNumber
                    min={0}
                    placeholder="Enter number of reviews"
                    style={{ width: '100%' }}
                />
            </Form.Item>

            <Form.Item
                label="Price"
                name="price"
                rules={[{ required: true, message: 'Please enter the price, $' }]}
            >
                <InputNumber
                    min={0}
                    placeholder="Enter price, $"
                    style={{ width: '100%' }}
                />
            </Form.Item>

            <Form.Item
                label="List Price"
                name="listPrice"
                rules={[{ required: true, message: 'Please enter the list price, $' }]}
            >
                <InputNumber
                    min={0}
                    placeholder="Enter list price"
                    style={{ width: '100%' }}
                />
            </Form.Item>

            <Form.Item>
                <Button type="primary" htmlType="submit" style={{ width: '100%' }}>
                    Submit Product
                </Button>
            </Form.Item>
        </Form>
    );
}