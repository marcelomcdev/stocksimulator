### STAGE 1: Build ###
FROM node:12.7-alpine AS build
WORKDIR /usr/src/app
COPY package.json package-lock.json ./
RUN npm install -g npm@7.5.4
COPY . .
RUN npm run build
### STAGE 2: Run ###
FROM nginx:1.17.1-alpine
COPY nginx.conf /etc/nginx/nginx.conf
COPY --from=build /usr/src/app/dist/stock-simulator /usr/share/nginx/html


# ### STAGE 1: Build ###
# FROM node:latest AS build

# WORKDIR /app
# ENV PATH=${PATH}:./node_modules/.bin
# ENV NODE_PATH=/app/node_modules
# ADD package.json /app
# ADD package-lock.json /app
# RUN npm ci
# RUN ngcc
# ADD . /app
# RUN npm run build:prod

# ### STAGE 2: Run ###
# FROM nginx:alpine
# VOLUME /var/cache/nginx
# COPY default.conf /etc/nginx/conf.d/default.conf
# COPY --from=build /app/dist/stock-simulator /usr/share/nginx/html
