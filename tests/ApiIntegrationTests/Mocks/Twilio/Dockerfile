FROM node:20-alpine

RUN npm install -g @mockoon/cli@9.3.0

# Set working directory
WORKDIR /app

# Copy the TwilioMocks.json file into the container
COPY TwilioMocks.json /app/TwilioMocks.json

# Expose the port Mockoon will listen on (default 3000)
EXPOSE 3000

# Run Mockoon with your JSON file
CMD ["mockoon-cli", "start", "--data", "/app/TwilioMocks.json", "--port", "3000"]

# Build this image with the command: docker build -t mockoon-twilio-mocks:latest .
