import { Button } from "antd";

interface LoadMoreButtonProps {
    onClick: () => void;   
    label: string;
}

export const LoadMoreButtonComponent = ({ onClick, label }: LoadMoreButtonProps) => {
    return (
        <div style={{ textAlign: "center", marginTop: "16px" }}>
            <Button
                type="primary"
                onClick={onClick}
            >
                {label}
            </Button>
        </div>
    );
}
