import React, { useEffect, useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import styled from "styled-components";
import AOS from "aos";
import "aos/dist/aos.css";

const PaymentContainer = styled.div`
  background-color: #f0f4f8;
  color: #333;
  padding: 20px;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  align-items: center;
  font-family: "Poppins", sans-serif;
`;

const PaymentHeader = styled.h2`
  margin-bottom: 20px;
  color: #0d47a1;
  font-size: 2.5em;
  text-transform: uppercase;
  font-weight: 700;
  animation: fadeIn 1s ease-in-out;
`;

const PaymentDetails = styled.div`
  background: linear-gradient(145deg, #ffffff, #e6e6e6);
  box-shadow: 10px 10px 20px #d1d1d1, -10px -10px 20px #ffffff;
  padding: 20px;
  border-radius: 15px;
  max-width: 600px;
  width: 100%;
  text-align: left;
  margin-bottom: 20px;
  animation: slideInUp 1.2s ease;
`;

const DetailRow = styled.p`
  font-size: 1.2em;
  margin: 10px 0;
  strong {
    color: #0d47a1;
  }
`;

const InputGroup = styled.div`
  margin: 10px 0;
`;

const Label = styled.label`
  display: block;
  margin-bottom: 5px;
  font-weight: bold;
  color: #555;
`;

const Input = styled.input`
  width: 100%;
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #ccc;
  box-sizing: border-box;
  font-size: 1em;
  margin-bottom: 10px;
  transition: all 0.3s ease;

  &:focus {
    border-color: #0d47a1;
    box-shadow: 0px 0px 5px rgba(13, 71, 161, 0.5);
  }
`;

const Select = styled.select`
  width: 100%;
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #ccc;
  box-sizing: border-box;
  font-size: 1em;
  margin-bottom: 10px;
`;

const Button = styled.button`
  padding: 12px 20px;
  background-color: #0d47a1;
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 1.2em;
  cursor: pointer;
  margin-top: 20px;
  width: 100%;
  box-shadow: 0px 5px 10px rgba(0, 0, 0, 0.2);
  transition: all 0.3s ease;

  &:hover {
    background-color: #1565c0;
    transform: translateY(-2px);
    box-shadow: 0px 8px 15px rgba(0, 0, 0, 0.3);
  }

  &:disabled {
    background-color: #b0bec5;
    cursor: not-allowed;
  }
`;

const ErrorMessage = styled.p`
  color: red;
  font-weight: bold;
  animation: fadeIn 0.5s ease-in-out;
`;

const Payment = () => {
  const location = useLocation();
  const navigate = useNavigate();

  const { totalAmount, carName, pickupDate, dropOffDate, reservationId } = location.state || {};

  const [paymentMethod, setPaymentMethod] = useState("Credit Card");
  const [cardNumber, setCardNumber] = useState("");
  const [expiryDate, setExpiryDate] = useState("");
  const [cvv, setCvv] = useState("");
  const [errorMessage, setErrorMessage] = useState("");

  useEffect(() => {
    AOS.init({ duration: 1000 });
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!cardNumber || !expiryDate || !cvv) {
      setErrorMessage("Please fill in all payment details.");
      return;
    }

    const paymentDetails = {
      reservationId: reservationId,
      amount: totalAmount,
      paymentDate: new Date().toISOString(),
      paymentMethod: paymentMethod,
      status: "Completed",
    };

    try {
      const response = await fetch("https://localhost:7173/api/Payment", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify(paymentDetails),
      });

      if (!response.ok) {
        throw new Error(`Payment API failed with status ${response.status}`);
      }

      alert("Payment successful!");
      navigate("/roadreadyhome");
    } catch (error) {
      console.error("Error posting payment details:", error);
      alert("Payment recorded, but failed to update the database.");
    }
  };

  if (!totalAmount || !reservationId) {
    return (
      <PaymentContainer>
        <PaymentHeader>Invalid Payment Details</PaymentHeader>
        <Button onClick={() => navigate("/")}>Go Back</Button>
      </PaymentContainer>
    );
  }

  return (
    <PaymentContainer>
      <PaymentHeader data-aos="fade-down">Payment Details</PaymentHeader>
      <PaymentDetails data-aos="zoom-in">
        <DetailRow>
          <strong>Car Name:</strong> {carName}
        </DetailRow>
        <DetailRow>
          <strong>Pickup Date:</strong> {pickupDate}
        </DetailRow>
        <DetailRow>
          <strong>Drop-off Date:</strong> {dropOffDate}
        </DetailRow>
        <DetailRow>
          <strong>Total Amount:</strong> ₹{totalAmount}
        </DetailRow>
      </PaymentDetails>

      {errorMessage && <ErrorMessage>{errorMessage}</ErrorMessage>}

      <form onSubmit={handleSubmit} data-aos="fade-up">
        <InputGroup>
          <Label>Payment Method</Label>
          <Select value={paymentMethod} onChange={(e) => setPaymentMethod(e.target.value)} required>
            <option value="Credit Card">Credit Card</option>
            <option value="Debit Card">Debit Card</option>
            <option value="PayPal">PayPal</option>
            <option value="Net Banking">Net Banking</option>
          </Select>
        </InputGroup>

        {paymentMethod !== "PayPal" && (
          <>
            <InputGroup>
              <Label>Card Number</Label>
              <Input
                type="text"
                value={cardNumber}
                onChange={(e) => setCardNumber(e.target.value)}
                placeholder="Enter your card number"
                required
              />
            </InputGroup>

            <InputGroup>
              <Label>Expiry Date</Label>
              <Input
                type="text"
                value={expiryDate}
                onChange={(e) => setExpiryDate(e.target.value)}
                placeholder="MM/YY"
                required
              />
            </InputGroup>

            <InputGroup>
              <Label>CVV</Label>
              <Input
                type="text"
                value={cvv}
                onChange={(e) => setCvv(e.target.value)}
                placeholder="Enter your CVV"
                required
              />
            </InputGroup>
          </>
        )}

        <Button type="submit" data-aos="zoom-in">
          Pay ₹{totalAmount}
        </Button>
      </form>
    </PaymentContainer>
  );
};

export default Payment;
