import ExpertEvaluation from "../models/ExpertEvaluation";
import Product from "../models/Product";
import { Form, Input, InputNumber } from 'antd';

interface EvaluationResultsFormComponentProps {
    product: Product;
    consensusOpinion: ExpertEvaluation;
    expectedSales: number;
}

export const EvaluationResultsFormComponent = ({ product, consensusOpinion, expectedSales }: EvaluationResultsFormComponentProps) => {
    return (
        <Form
            layout="vertical"
            style={{ maxWidth: 400, margin: "0 auto" }}
            initialValues={{
                title: product.title,
                stars: product.stars,
                reviews: product.reviews,
                price: product.price,
                listPrice: product.listPrice
            }}
        >
            <Form.Item label="Title" name="title" style={{ marginBottom: "8px" }}>
                <Input value={product.title} readOnly />
            </Form.Item>

            <Form.Item label="Stars" name="stars" style={{ marginBottom: "8px" }}>
                <InputNumber value={product.stars} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="Reviews" name="reviews" style={{ marginBottom: "8px" }}>
                <InputNumber value={product.reviews} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="Price" name="price" style={{ marginBottom: "8px" }}>
                <InputNumber value={product.price} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="List Price" name="listPrice" style={{ marginBottom: "8px" }}>
                <InputNumber value={product.listPrice} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="Price Strategy" style={{ marginBottom: "8px" }}>
                <InputNumber value={consensusOpinion.priceStrategy} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="Demand" style={{ marginBottom: "8px" }}>
                <InputNumber value={consensusOpinion.demand} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="Quality" style={{ marginBottom: "8px" }}>
                <InputNumber value={consensusOpinion.quality} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="Price-Quality Ratio" style={{ marginBottom: "8px" }}>
                <InputNumber value={consensusOpinion.priceQuality} readOnly style={{ width: '100%' }} />
            </Form.Item>

            <Form.Item label="Expected Sales" style={{ marginBottom: "8px" }}>
                <InputNumber value={expectedSales} readOnly style={{ width: '100%' }} />
            </Form.Item>
        </Form>
    );
}
