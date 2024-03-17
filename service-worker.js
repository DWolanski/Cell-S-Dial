const CACHE_NAME = `cell-s-dial-v1`;

// Use the install event to pre-cache all initial resources.
self.addEventListener('install', event => {
  event.waitUntil((async () => {
    const cache = await caches.open(CACHE_NAME);
    cache.addAll([
      '/',
      'js/sketch.js',
      'js/scripts/cellularAutomata.js'
    ]);
  })());
});

self.addEventListener('fetch', function(event) {
  event.respondWith(
     caches.match(event.request)
       .then(function(response) {
         // Cache hit - return response
         if (response) {
           return response;
         }
         return fetch(event.request);
       }
     )
  );
 });