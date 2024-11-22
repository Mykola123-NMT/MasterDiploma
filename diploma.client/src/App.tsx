import './App.css';
import { Flex, Layout, Menu } from 'antd';
import { ProductListComponent } from './components/ProductList';
const { Header, Content } = Layout;
import { useState } from 'react';
import { EvaluationComponent } from './components/Evaluation';

function App() {
    const [activeKey, setActiveKey] = useState('1');

    const onMenuClick = (e: { key: string }) => {
        setActiveKey(e.key);
    };

    const items = [
        {
            key: '1',
            label: 'View Products'
        },
        {
            key: '2',
            label: 'Evaluate Product'
        },
    ];

    return (
        <Layout style={{ minHeight: "100vh" }}>
            <Header style={{ display: "flex" }}>
                <div style={{ color: "white", fontSize: "24px", marginRight: "5px" }}>Product Rating App</div>
                <Menu
                    theme="dark"
                    mode="horizontal"
                    defaultSelectedKeys={['1']}
                    items={items}
                    style={{ flex: 1, minWidth: 0 }}
                    onClick={onMenuClick}
                />
            </Header>
            <Content style={{ padding: "20px", backgroundColor: "darkseagreen" }}>
                {activeKey === "1" && <Flex vertical gap="middle"><ProductListComponent /></Flex>}
                {activeKey === "2" && <EvaluationComponent />}
            </Content>
        </Layout>
    );
}

export default App;
