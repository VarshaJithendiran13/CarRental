import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import styled from "styled-components";
import AOS from "aos";
import "aos/dist/aos.css";

// Styled Components
const Container = styled.div`
  background-color: #121212;
  color: #e0e0e0;
  padding: 20px;
  min-height: 100vh;
  font-family: "Roboto", sans-serif;
  display: flex;
  flex-direction: column;
  align-items: center;
`;

const Form = styled.form`
  width: 100%;
  max-width: 600px;
  background-color: #1e1e1e;
  padding: 30px;
  border-radius: 15px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3);
  animation: fadeInUp 1s ease-in-out;
`;

const InputGroup = styled.div`
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
`;

const Label = styled.label`
  font-size: 1.2em;
  margin-bottom: 5px;
  color: #ffffff;
`;

const Input = styled.input`
  padding: 12px;
  border: 1px solid #ccc;
  border-radius: 10px;
  font-size: 1.1em;
  background-color: #333333;
  color: white;
  outline: none;
  transition: background-color 0.3s ease;

  &:focus {
    background-color: #444444;
  }
`;

const TextArea = styled.textarea`
  padding: 12px;
  border: 1px solid #ccc;
  border-radius: 10px;
  font-size: 1.1em;
  background-color: #333333;
  color: white;
  resize: none;
  transition: background-color 0.3s ease;

  &:focus {
    background-color: #444444;
  }
`;

const Button = styled.button`
  padding: 12px 25px;
  background-color: #0d47a1;
  color: white;
  border: none;
  border-radius: 25px;
  font-size: 1.2em;
  cursor: pointer;
  transition: background-color 0.3s ease;

  &:hover {
    background-color: #1565c0;
  }
`;

const Title = styled.h2`
  color: #fff;
  font-size: 2.5em;
  margin-bottom: 30px;
  text-shadow: 2px 2px 6px rgba(0, 0, 0, 0.6);
  animation: fadeInDown 1s ease-in-out;
`;

const PostReview = () => {
  const navigate = useNavigate();
  const { state } = useLocation();
  const { carId } = state;

  const [rating, setRating] = useState(5);
  const [comment, setComment] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    const reviewDate = new Date().toISOString();

    // Prepare review data (no need for userId)
    const reviewData = {
      carId: carId,
      rating: rating,
      comment: comment,
      reviewDate: reviewDate,
    };

    try {
      const response = await fetch("https://localhost:7173/api/Review", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Authorization: `Bearer ${localStorage.getItem("token")}`,
        },
        body: JSON.stringify(reviewData),
      });

      if (!response.ok) {
        throw new Error("Failed to post review.");
      }

      alert("Review posted successfully!");
      navigate(`/reservenow/${carId}`); // Redirect back to the ReserveNow page
    } catch (error) {
      console.error(error.message);
      alert("An error occurred while posting the review.");
    }
  };

  return (
    <Container>
      <Title data-aos="fade-up">Write a Review</Title>

      <Form onSubmit={handleSubmit} data-aos="zoom-in">
        <InputGroup data-aos="fade-left">
          <Label htmlFor="rating">Rating (1-5)</Label>
          <Input
            type="number"
            id="rating"
            value={rating}
            onChange={(e) => setRating(Number(e.target.value))}
            min="1"
            max="5"
            required
          />
        </InputGroup>

        <InputGroup data-aos="fade-right">
          <Label htmlFor="comment">Comment</Label>
          <TextArea
            id="comment"
            rows="5"
            value={comment}
            onChange={(e) => setComment(e.target.value)}
            required
          />
        </InputGroup>

        <Button type="submit" data-aos="fade-up">Submit Review</Button>
      </Form>
    </Container>
  );
};

export default PostReview;
