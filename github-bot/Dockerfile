FROM node:10

WORKDIR /app
COPY package* ./
RUN npm install --production
COPY . .

ENTRYPOINT ["/app/node_modules/.bin/probot", "receive"]
CMD ["/app/index.js"]