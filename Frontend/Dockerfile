### STAGE 1: Build ###
FROM node:latest AS build

WORKDIR /app
ENV PATH=${PATH}:./node_modules/.bin
ENV NODE_PATH=/app/node_modules
ADD package.json /app
ADD package-lock.json /app
RUN npm ci
RUN ngcc
ADD . /app
RUN npm run build:prod

### STAGE 2: Run ###
FROM nginx:alpine
VOLUME /var/cache/nginx
COPY default.conf /etc/nginx/conf.d/default.conf
COPY --from=build /app/dist/stock-simulator /usr/share/nginx/html
