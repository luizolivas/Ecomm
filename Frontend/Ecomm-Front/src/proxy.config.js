const PROXY_CONFIG = [
    {
        context: [
            '/api'
        ] ,
        target: 'https://localhost:44334/',
        secure: false,
        changeOrigin: true,
        pathRewrite:{
            "^/": ""
        }
    }
];

module.exports = PROXY_CONFIG;