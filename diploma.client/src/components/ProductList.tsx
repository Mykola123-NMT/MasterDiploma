import Product from "../models/Product";
import { ProductComponent } from "./Product";
import { Flex } from 'antd';
import productApi from '../api/productApi';
import { useState, useEffect } from 'react';
import { LoadMoreButtonComponent } from '../components/LoadMoreButton';

export const ProductListComponent = () => {
    const [productList, setProductList] = useState<Product[]>([]);
    const [currentPage, setCurrentPage] = useState(1);

    useEffect(() => {
        loadProducts(currentPage);
    }, [currentPage]);

    const loadProducts = async (page: number) => {
        const response = await productApi.getProductsPerPage(page, 50);
        setProductList((prevList) => [...prevList, ...response.$values]);
    }

    const handleLoadMore = () => {
        setCurrentPage((prevPage) => {
            const nextPage = prevPage + 1;
            return nextPage;
        });
    };

    return (
        <Flex vertical gap="middle">
            <Flex gap="35px" wrap justify="space-around">
                {productList.map((product, index) => (
                    <ProductComponent key={index} product={product} />
                ))}                
            </Flex>   
            <LoadMoreButtonComponent onClick={handleLoadMore} label="Load More" />
        </Flex>                           
    );
}
